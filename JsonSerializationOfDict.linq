<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var testJson = "{\r\n\t\"https\": {\r\n\t\t\"CPAS/Queue/PCI/v1\": [{\r\n\t\t\t\t\"ClientData\": {\r\n\t\t\t\t\t\"Protocol\": \"https\",\r\n\t\t\t\t\t\"Endpoint\": \"172.27.145.197:6100/api/ping\",\r\n\t\t\t\t\t\"Command\": \"GET\",\r\n\t\t\t\t\t\"SuccessfulResponses\": \"200,204\",\r\n\t\t\t\t\t\"NumberOfSuccessiveFailuresBeforeDisabling\": \"10\",\r\n\t\t\t\t\t\"PingInterval\": \"2\",\r\n\t\t\t\t\t\"DisableThreshhold\": \"10\",\r\n\t\t\t\t\t\"DisabledWaitInterval\": \"600\",\r\n\t\t\t\t\t\"Custom\": {\r\n\t\t\t\t\t\t\"SlackAlertUrl\": \"https://hooks.slack.com/services/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\",\r\n\t\t\t\t\t\t\"SlackAlertChannel\": \"#cpas_alerts\",\r\n\t\t\t\t\t\t\"Dependencies\": [\"CPAS/FirstData/PCI/v1\"]\r\n\t\t\t\t\t}\r\n\t\t\t\t},\r\n\t\t\t\t\"IsDisabled\": false,\r\n\t\t\t\t\"LastResponseWasSuccessful\": true,\r\n\t\t\t\t\"NumberFailedConsecutiveAttempts\": 0,\r\n\t\t\t\t\"LastChecked\": \"2017-11-22T19:13:16.9599511Z\"\r\n\t\t\t}\r\n\t\t],\r\n\t\t\"CPAS/OData/PCI/v1\": [{\r\n\t\t\t\t\"ClientData\": {\r\n\t\t\t\t\t\"Protocol\": \"https\",\r\n\t\t\t\t\t\"Endpoint\": \"172.27.145.197:5100/api/ping\",\r\n\t\t\t\t\t\"Command\": \"GET\",\r\n\t\t\t\t\t\"SuccessfulResponses\": \"200,204\",\r\n\t\t\t\t\t\"NumberOfSuccessiveFailuresBeforeDisabling\": \"10\",\r\n\t\t\t\t\t\"PingInterval\": \"2\",\r\n\t\t\t\t\t\"DisableThreshhold\": \"10\",\r\n\t\t\t\t\t\"DisabledWaitInterval\": \"600\"\r\n\t\t\t\t\t\"Custom\": {\r\n\t\t\t\t\t\t\"SlackAlertUrl\": \"https://hooks.slack.com/services/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\",\r\n\t\t\t\t\t\t\"SlackAlertChannel\": \"#cpas_alerts\",\r\n\t\t\t\t\t\t\"Dependencies\": [\"CPAS/Queue/PCI/v1\"]\r\n\t\t\t\t\t}\r\n\t\t\t\t},\r\n\t\t\t\t\"IsDisabled\": false,\r\n\t\t\t\t\"LastResponseWasSuccessful\": true,\r\n\t\t\t\t\"NumberFailedConsecutiveAttempts\": 0,\r\n\t\t\t\t\"LastChecked\": \"2017-11-22T19:13:16.9599511Z\"\r\n\t\t\t}\r\n\t\t],\r\n\t\t\"CPAS/FirstData/PCI/v1\": [{\r\n\t\t\t\t\"ClientData\": {\r\n\t\t\t\t\t\"Protocol\": \"https\",\r\n\t\t\t\t\t\"Endpoint\": \"172.27.145.197:6100/api/FirstData/ping\",\r\n\t\t\t\t\t\"Command\": \"GET\",\r\n\t\t\t\t\t\"SuccessfulResponses\": \"200,204\",\r\n\t\t\t\t\t\"NumberOfSuccessiveFailuresBeforeDisabling\": \"10\",\r\n\t\t\t\t\t\"PingInterval\": \"2\",\r\n\t\t\t\t\t\"DisableThreshhold\": \"10\",\r\n\t\t\t\t\t\"DisabledWaitInterval\": \"600\",\r\n\t\t\t\t\t\"Custom\": {\r\n\t\t\t\t\t\t\"SlackAlertUrl\": \"https://hooks.slack.com/services/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\",\r\n\t\t\t\t\t\t\"SlackAlertChannel\": \"#cpas_alerts\",\r\n\t\t\t\t\t\t\"Dependencies\": []\r\n\t\t\t\t\t}\r\n\t\t\t\t},\r\n\t\t\t\t\"IsDisabled\": true,\r\n\t\t\t\t\"LastResponseWasSuccessful\": false,\r\n\t\t\t\t\"NumberFailedConsecutiveAttempts\": 163,\r\n\t\t\t\t\"LastChecked\": \"2017-11-22T19:13:16.9609534Z\"\r\n\t\t\t}\r\n\t\t]\r\n\t},\r\n\t\"http\": {\r\n\t\t\"CPAS/Test2\": [{\r\n\t\t\t\t\"ClientData\": {\r\n\t\t\t\t\t\"Protocol\": \"https\",\r\n\t\t\t\t\t\"Endpoint\": \"localhost:8080\",\r\n\t\t\t\t\t\"Command\": \"GET\",\r\n\t\t\t\t\t\"SuccessfulResponses\": \"200,204\",\r\n\t\t\t\t\t\"NumberOfSuccessiveFailuresBeforeDisabling\": \"10\",\r\n\t\t\t\t\t\"PingInterval\": \"2\",\r\n\t\t\t\t\t\"DisableThreshhold\": \"10\",\r\n\t\t\t\t\t\"DisabledWaitInterval\": \"600\",\r\n\t\t\t\t\t\"Custom\": {\r\n\t\t\t\t\t\t\"SlackAlertUrl\": \"https://hooks.slack.com/services/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\",\r\n\t\t\t\t\t\t\"SlackAlertChannel\": \"#cpas_alerts\",\r\n\t\t\t\t\t\t\"Dependencies\": [\"CPAS/FirstData/PCI/v1\"]\r\n\t\t\t\t\t}\r\n\t\t\t\t},\r\n\t\t\t\t\"IsDisabled\": true,\r\n\t\t\t\t\"LastResponseWasSuccessful\": false,\r\n\t\t\t\t\"NumberFailedConsecutiveAttempts\": 127,\r\n\t\t\t\t\"LastChecked\": \"2017-11-22T19:13:11.8367316Z\"\r\n\t\t\t}\r\n\t\t],\r\n\t\t\"CPAS/Test3\": [{\r\n\t\t\t\t\"ClientData\": {\r\n\t\t\t\t\t\"Protocol\": \"https\",\r\n\t\t\t\t\t\"Endpoint\": \"172.27.145.197:5100/api/ping\",\r\n\t\t\t\t\t\"Command\": \"GET\",\r\n\t\t\t\t\t\"SuccessfulResponses\": \"200,204\",\r\n\t\t\t\t\t\"NumberOfSuccessiveFailuresBeforeDisabling\": \"10\",\r\n\t\t\t\t\t\"PingInterval\": \"2\",\r\n\t\t\t\t\t\"DisableThreshhold\": \"10\",\r\n\t\t\t\t\t\"DisabledWaitInterval\": \"600\",\r\n\t\t\t\t\t\"Custom\": {\r\n\t\t\t\t\t\t\"SlackAlertUrl\": \"https://hooks.slack.com/services/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\",\r\n\t\t\t\t\t\t\"SlackAlertChannel\": \"#cpas_alerts\",\r\n\t\t\t\t\t\t\"Dependencies\": [\"CPAS/Queue/PCI/v1\"]\r\n\t\t\t\t\t}\r\n\t\t\t\t},\r\n\t\t\t\t\"IsDisabled\": false,\r\n\t\t\t\t\"LastResponseWasSuccessful\": true,\r\n\t\t\t\t\"NumberFailedConsecutiveAttempts\": 0,\r\n\t\t\t\t\"LastChecked\": \"2017-11-22T19:13:16.9599511Z\"\r\n\t\t\t}\r\n\t\t]\r\n\t}\r\n}\r\n";

var testDict = new Dictionary<string, Dictionary<string, List<object>>>();

var testObj1 = new { test1 = "one", test2 = "two" };
var testObj2 = new { test1 = "three", test2 = "four" };

var testObj3 = new { test1 = "five", test2 = "six" };
var testObj4 = new { test1 = "seven", test2 = "eight" };

var obj1Dict = new Dictionary<string, List<object>> { { "/one/two/", new List<object> { testObj1, testObj3 } } };
var obj2Dict = new Dictionary<string,  List<object>> { { "/three/four/",  new List<object> { testObj2, testObj4 }} };

testDict.Add("HTTPS", obj1Dict);
testDict["HTTPS"].Add("/three/four/", new List<object> { testObj2, testObj4 });

var result = JsonConvert.SerializeObject(testDict);
result.Dump();

var result2 = JsonConvert.DeserializeObject<IDictionary<string, IDictionary<string, List<object>>>>(result);
result2.Dump();

var newObjKey = "/hello/there/from/ne";
var newObjValue = new[] {"value1", "value2", "value3"};

var testObjKey = (JValue)JToken.FromObject(newObjKey)

var testObjValue = new JObject(newObjValue);
var totalNewObj = new JObject(testObjKey, testObjKey);

totalNewObj.Dump();
