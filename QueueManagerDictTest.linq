<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

private static MqConfig MqConfigTest = new MqConfig();

void Main()
{

	using (var fileStream = File.OpenText(@"C:\Users\bw6y\Documents\LINQPad Queries\mqTest.json"))
	{
		var myFile = fileStream.ReadToEnd();
		MqConfigTest = JsonConvert.DeserializeObject<MqConfig>(myFile);
	}

	MqConfigTest.Dump();
}

//public static class TestInsertMq {
//	public static void InsertMq()
//	{
//		var myMq = new MqConfig();
//		myMq.Mq = new List<UserQuery.QueueManagerConfig>();
//		var queueManagerConfig1 = new QueueManagerConfig();
//
//		var queueManagerConfigList1 = new List<MqGroupConfig> {
//			new MqGroupConfig {Type = "fdr", QueueManager = "queueManager1"},
//			new MqGroupConfig {Type = "fdr", QueueManager = "queueManager2"}
//		};
//
//
//		queueManagerConfig1.Add("group1", queueManagerConfigList1);
//
//		var queueManagerConfig2 = new QueueManagerConfig();
//
//		var queueManagerConfigList2 = new List<MqGroupConfig> {
//			new MqGroupConfig {Type = "zoot", QueueManager = "queueManager3"},
//			new MqGroupConfig {Type = "zoot", QueueManager = "queueManager4"}
//		};
//
//		queueManagerConfig1.Add("group2", queueManagerConfigList2);
//
//		myMq.Mq.AddRange(new[] { queueManagerConfig1, queueManagerConfig2 });
//
//		myMq.Dump();
//	}
//}

public class MqConfig
{
	public List<MqGroupConfig> Mq { get; set; }
	public PolicySettings PolicySettings { get; set; }
}

public class MqGroupConfig : Dictionary<string, List<QueueManagerConfig>>
{
	
}

public class QueueManagerConfig
{
	public string Type { get; set; }
	public string Group { get; set; }
	public string Host { get; set; }
	public int Port { get; set; }
	public string Channel { get; set; }
	public string QueueManager { get; set; }
	public string InQueue { get; set; }
	public string OutQueue { get; set; }
	public int Timeout { get; set; }
}

public class PolicySettings
{
	public int WaitAndRetryInterval { get; set; }
	public int ExceptionsAllowed { get; set; }
	public int BreakInterval { get; set; }
}