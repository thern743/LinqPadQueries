<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

private static string _existingData = "[{		\"Path\": \"/\",		\"Data\": null,		\"Nodes\": [{				\"Path\": \"rock\",				\"Data\": null,				\"Nodes\": [{						\"Path\": \"you\",						\"Data\": null,						\"Nodes\": [{								\"Path\": \"like\",								\"Data\": null,								\"Nodes\": [{										\"Path\": \"hurricane\",										\"Data\": {											\"hello\": \"world\",											\"first\": \"last\",											\"myData\": \"123456\"										},										\"Nodes\": []									}								]							}						]					}				]			}		]	}]";
private List<DirectoryTree> _treeData;

void Main()
{
	_treeData = JsonConvert.DeserializeObject<List<DirectoryTree>>(_existingData);
	_treeData.Dump();
	var data = "{	\"hello\":\"world\",	\"first\":\"tony\",	\"last\":\"hernandez\"}";
	var routeValues = "/rock/you/more/like/hurricane";
	var listData = BuildList(routeValues, data);
	var stringData = JsonConvert.SerializeObject(listData);
	var jTokenData = JsonConvert.DeserializeObject<JToken>(stringData);
	jTokenData.Dump();
}

private DirectoryTree BuildList(string routeValues, JToken data)
{
	var myTree = new DirectoryTree() { Path = "/", Nodes = new List<DirectoryTree>() };
	var parts = routeValues.Split('/');
	EnsureExists(myTree, parts, data);
	return myTree;
}

private void EnsureExists(DirectoryTree myTree, IEnumerable<string> routeValues, JToken data)
{
	if (routeValues.Any())
	{
		var path = routeValues.First();
		var node = myTree.Nodes.SingleOrDefault(x => x.Path == path);
		if (node == null)
		{
			node = new DirectoryTree() { Path = path, Nodes = new List<DirectoryTree>() };
			myTree.Nodes.Add(node);
		}

		EnsureExists(node, routeValues.Skip(1), data);
	}
	else
	{
		AddDataToLastNode(myTree, data);
	}
}

private void AddDataToLastNode(DirectoryTree myTree, JToken data)
{
	myTree.Data = data;
}

public class DirectoryTree
{
	public string Path { get; set; }
	public object Data { get; set; }
	public List<DirectoryTree> Nodes { get; set; }

	public override string ToString()
	{
		var fullPath = Path == "/" ? string.Empty : $"{Path}";
		return Nodes.Aggregate(fullPath, (current, node) => current + $"/{node.Path}");
	}
}