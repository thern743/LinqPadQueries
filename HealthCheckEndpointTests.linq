<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
	var test = "{	\"Endpoint\": \"https://172.27.145.197:6100/api/ping\",	\"Command\": \"GET\",	\"SuccessfulResponses\": \"200,204\",	\"NumberOfSuccessiveFailuresBeforeDisabling\": \"10\",	\"PingInterval\": \"2\",	\"DisableThreshhold\": \"10\",	\"DisabledWaitInterval\": \"600\",						\"SlackAlertUrl\": \"https://hooks.slack.com/services/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\",	\"SlackAlertChannel\": \"#cpas_alerts\",	\"Dependencies\": [\"CPAS/FirstData/PCI/v1\"]					}";
	
	var httpEndpointProperties = typeof(HttpEndpoint).GetProperties().Select(x => x.Name);
	
	var token = JToken.Parse(test);
	var jObejct = token.Value<JObject>();
	var unmappedProperties = new Dictionary<string, object>();
	
	foreach(var property in jObejct) {
		if(!httpEndpointProperties.Contains(property.Key))
			unmappedProperties.Add(property.Key, property.Value);
	}
	
	unmappedProperties.Dump();
	
	var endpoint = token.ToObject<HttpEndpoint>();
	endpoint.Dump();
}

public class HttpEndpoint
{
	public string Endpoint { get; set; }
	public string Command { get; set; }
	public string SuccessfulResponses { get; set; }
	public string NumberOfSuccessiveFailuresBeforeDisabling { get; set; }
	public int PingInterval { get; set; }
	public int DisableThreshhold { get; set; }
	public int DisabledWaitInterval { get; set; }
}