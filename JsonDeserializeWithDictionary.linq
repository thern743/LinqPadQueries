<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

protected static ConcurrentDictionary<string, ConcurrentDictionary<string, List<HealthRegistration>>> HealthRegistrations;

void Main()
{
	using(var fileStream = File.OpenText(@"C:\Users\obsidian\Documents\LINQPad Queries\test.json")) {
		var myFile = fileStream.ReadToEnd();
		HealthRegistrations = JsonConvert.DeserializeObject<ConcurrentDictionary<string, ConcurrentDictionary<string, List<HealthRegistration>>>>(myFile);		
	}
	
	HealthRegistrations.Dump();

	var category = "category one";
	var path = "path 1";
	
	var newEntry = new HealthRegistration
	{
		ClientData = JToken.FromObject(new
		{
			Endpoint = "new endpoint value 10",
			Command = "new value1 1"
		}),
		Health = new HealthStatus()
	};

	SaveOrUpdate(category, path, newEntry);
	
	var updateEntry = new HealthRegistration
	{
		ClientData = JToken.FromObject(new 
		{
			Endpoint = "endpoint value 1",
			Command = "updated other key value"
		})
	};
	
	SaveOrUpdate(category, path, updateEntry);

	var newEntry2 = new HealthRegistration
	{
		ClientData = JToken.FromObject(new
		{
			Endpoint = "another new entry",
			Command = "another new value1 1"
		})
	};

	SaveOrUpdate(category, "path 9", newEntry2);

	var newEntry3 = new HealthRegistration
	{
		ClientData = JToken.FromObject(new
		{
			Endpoint = "yet another new entry",
			Command = "yet another new value1 1"
		})
	};

	SaveOrUpdate("category three", "path 77", newEntry3);
}

void SaveOrUpdate(string category, string path, HealthRegistration entry)
{
	var newListEntry = new List<HealthRegistration> { entry };
	var newDictEntry = new ConcurrentDictionary<string, List<HealthRegistration>>(
		new Dictionary<string, List<HealthRegistration>> { { path, newListEntry } }
	);

	if (HealthRegistrations.ContainsKey(category) && HealthRegistrations[category].ContainsKey(path))
	{
		var endpoint = entry.ClientData.Value<string>("Endpoint");
		var existinValues = HealthRegistrations[category][path].ToDictionary(x => x.ClientData.Value<string>("Endpoint"));
		HealthRegistration existingEntry;

		if (existinValues.TryGetValue(endpoint, out existingEntry)) 
		{
			existingEntry.ClientData = entry.ClientData;
		}
		else 
		{
			HealthRegistrations[category][path].AddRange(new List<HealthRegistration> { entry });
		}
	}
	else if (HealthRegistrations.ContainsKey(category) && !HealthRegistrations[category].ContainsKey(path))
	{
		HealthRegistrations[category].AddOrUpdate(path,
			(pathKey) => newListEntry,
			(pathKey, pathValue) => newListEntry
		);
	}
	else
	{
		HealthRegistrations.AddOrUpdate(category,
			(pathKey) => newDictEntry,
			(pathKey, pathValue) => newDictEntry
		);
	}
	
	HealthRegistrations.Dump();
}

public class HealthRegistration
{
	public JToken ClientData { get; set; }
	public HealthStatus Health { get; set; }
}

public class HealthStatus
{
	public int FailedAttempts { get; set;}
	public DateTime LastChecked { get; set;}
}