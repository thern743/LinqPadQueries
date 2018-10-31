<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var fileName = $"C:\\Users\\bw6y\\Documents\\LINQPad Queries\\sqs.json";
	
	using (var file = File.OpenText(fileName))
	{
		var json = file.ReadToEndAsync();
		json.Dump();
		
		var objData = JsonConvert.DeserializeObject<LevelingEvent>(json.Result);		
		objData.Dump();
		
		var chds = JsonConvert.DeserializeObject<LevelingEventBody>(objData.Records[0].Body);
		chds.Dump();
	}
}

public class LevelingEvent
{
	public LevelingEventRecord[] Records { get; set; }
}

public class LevelingEventRecord
{
	[JsonProperty("messageId")]
	public string MessageId { get; set; }
	[JsonProperty("body")]
	public string Body { get; set; }
	[JsonProperty("attributes")]
	public IDictionary<string, string> Attributes { get; set; }
}

public class LevelingEventBody
{
	[JsonProperty("chds", Required = Required.Always)]
	public IEnumerable<string> Chds { get; set; }
}