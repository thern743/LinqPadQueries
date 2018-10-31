<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
	var fileName = $"C:\\Users\\bw6y\\Documents\\LINQPad Queries\\test.json";
	ConcurrentDictionary<string, ConcurrentDictionary<string, List<HealthRegistration>>> objData;

	using (var file = File.OpenText(fileName))
	{
		var json = file.ReadToEndAsync();
		objData = JsonConvert.DeserializeObject<ConcurrentDictionary<string, ConcurrentDictionary<string, List<HealthRegistration>>>>(json.Result);
		objData.Dump();
	}

	var pathClientData =
				from protocolEndpoint in objData
				select new
				{
					Protocol = protocolEndpoint.Key,
					PathEndpoints = protocolEndpoint.Value
				} into protocolPathEndpoints
				from pathEndpoint in protocolPathEndpoints.PathEndpoints
				let path = $"testing/{protocolPathEndpoints.Protocol}/{pathEndpoint.Key}"
				let clientData = JTokenHelper.DistinctPropertyNames(pathEndpoint.Value)
				select new
				{
					path,
					clientData
				};

	var paths = pathClientData.ToDictionary(
		endpointPath => endpointPath.path,
		endpointPath => endpointPath.clientData.ToList()
	);
	
	paths.Dump();
}

public class HealthRegistration
{
	public JToken ClientData { get; set; }
	public HealthStatus HealthStatus {get; set;}
}

public class HealthStatus
{
	public bool IsDisabled { get; set; }
	public bool LastResponseWasSuccessful { get; set; }
	public int NumberFailedConsecutiveAttempts { get; set; }
	public DateTime LastChecked { get; set; }
}

public class JTokenHelper
{
	public static IEnumerable<string> DistinctPropertyNames(IEnumerable<HealthRegistration> registrations)
	{
		return registrations.SelectMany(x => x.ClientData.Value<JObject>().Properties().Select(z => z.Name)).Distinct();
	}
}