<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var test = new CpasQueryOptions {
		DataEntityType = "CAP",
		Select = new string[] {"ENTR_ID", "SYS_ID"},
	 	Where = "ENTR_ID eq '4147211025050336' and SYS_ID eq '5107'"
	};
	
	var temp = JsonConvert.SerializeObject(test);
	temp.Dump();
}

public class CpasQueryOptions
{
	public string DataEntityType { get; set; }
	public IEnumerable<string> Select { get; set; }
	public string Where { get; set; }
}
