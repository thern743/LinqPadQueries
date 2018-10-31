<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var fileName = $"C:\\Users\\bw6y\\Documents\\LINQPad Queries\\test2.json";
	Dictionary<string, Dictionary<string, List<HealthRegistration>>> objData;

	using (var file = File.OpenText(fileName))
	{
		var json = file.ReadToEndAsync();
		objData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<HealthRegistration>>>>(json.Result);
		//objData.Dump();
	}

	var here = objData.ToDictionary(
				kvp => kvp.Key,
				kvp => kvp.Value.ToDictionary(kvp2 => kvp2.Key, BuildDict));
				
	here.Dump();
}

private Dictionary<string, object> BuildDict(KeyValuePair<string, List<HealthRegistration>> list)
{	
	var finalResult = new Dictionary<string, object>();
	
	var result = list.Value.SelectMany(x => x.ClientData).ToDictionary(x => x.Path);
	var result2 = list.Value.Select(x => x.Health).ToDictionary(kvp => "Health", kvp => kvp); //.SelectMany(x => JToken.FromObject(x)).ToDictionary(v => v.Path);
	
	foreach(var r in result) {
		finalResult.Add(r.Key, r.Value);	
	}

	foreach (var r in result2)
	{
		finalResult.Add(r.Key, r.Value);
	}

	//result.Dump();
	//result2.Dump();
	//finalResult.Dump();
	return finalResult;
}

public class HealthRegistration
{
	public JToken ClientData { get; set; }
	public HealthStatus Health { get; set; }
}

public class HealthStatus
{
	public bool IsDisabled { get; set; }
	public bool LastResponseWasSuccessful { get; set; }
	public int ConsecutiveFailureNumber { get; set; }
	public int ConsecutiveSuccessNumber { get; set; }
	public DateTime LastChecked { get; set; }
}
