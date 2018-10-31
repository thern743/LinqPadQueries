<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var testData = @"{
			'Command': 'GET',
			'Protocol': 'https',
			'EndPoint': '172.27.145.155:5100/api/ping',
			'SuccessfulResponses' = '200,204',
            'NumberOfSuccessiveFailuresBeforeDisabling': '10',
            'PingInterval': '300',
			'DisableThreshhold': '10',
			'SlackAlertUrl': 'https://hooks.slack.com/services/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',
			'SlackAlertChannel': '#cpas_alerts'
        }";
		
var token = JToken.Parse(testData);

var myProperties = new[] { "endpoint", "protocol", "command" };

var jObject = token.Value<JObject>();
var clientData = jObject.Properties()
	.Where(property => myProperties.Contains(property.Name.ToLowerInvariant()))
	.ToDictionary(property => property.Name.ToLowerInvariant(), property => property.Value);

var endpoint = clientData["endpoint"];
var protocol = clientData["protocol"];
var command = clientData["command"];

endpoint.Dump();
protocol.Dump();
command.Dump();