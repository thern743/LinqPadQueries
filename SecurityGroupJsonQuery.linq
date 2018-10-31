<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var fileName = $"C:\\Users\\bw6y\\Documents\\LINQPad Queries\\securityGroups.json";
	object objData;

	using (var file = File.OpenText(fileName))
	{
		var json = file.ReadToEndAsync();
		objData = JsonConvert.DeserializeObject<object>(json.Result);
		objData.Dump();
	}
}

// Define other methods and classes here
