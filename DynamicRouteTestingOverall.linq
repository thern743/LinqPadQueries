<Query Kind="Program" />

void Main()
{
	var resource = "/credit/rewards/leveling/durable/{durable}/{action}/{level}";
	
	var route1 = new LambdaRouteAttribute("/credit/rewards/leveling/durable/{durable}/upgrade/icon");
	var route2 = new LambdaRouteAttribute("/credit/rewards/leveling/durable/{durable}/upgrade/five");
	var route3 = new LambdaRouteAttribute("/credit/rewards/leveling/durable/{durable}/{action}/other");
	var route4 = new LambdaRouteAttribute("/credit/rewards/leveling/durable/{durable}/bazbog/{level}");
	var route5 = new LambdaRouteAttribute("/credit/rewards/leveling/durable/{durable}/{action}/six");
	var route6 = new LambdaRouteAttribute("/credit/rewards/leveling/durable/{durable}/{action}/seven");
	var routes = new List<LambdaRouteAttribute> { route1, route2, route3, route4, route5, route6 };
	
	var path1 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/upgrade/icon";
	var pathParameters1 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "upgrade" }, { "level", "icon" }};
	
	var path2 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/upgrade/five";
	var pathParameters2 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "upgrade" }, { "level", "five" } };

	var path3 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/foobar/other";
	var pathParameters3 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "foobar" }, { "level", "other" } };

	var path4 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/bazbog/hello_world";
	var pathParameters4 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "bazbog" }, { "level", "hello_world" } };

	var path5 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/upgrade/six";
	var pathParameters5 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "upgrade" }, { "level", "six" } };

	var path6 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/upgrade/seven";
	var pathParameters6 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "upgrade" }, { "level", "seven" } };

	routes.FirstOrDefault(x => isMatch(x, path1, pathParameters1)).Dump();
	routes.FirstOrDefault(x => isMatch(x, path2, pathParameters2)).Dump();
	routes.FirstOrDefault(x => isMatch(x, path3, pathParameters3)).Dump();
	routes.FirstOrDefault(x => isMatch(x, path4, pathParameters4)).Dump();
	routes.FirstOrDefault(x => isMatch(x, path5, pathParameters5)).Dump();
	routes.FirstOrDefault(x => isMatch(x, path6, pathParameters6)).Dump();

	bool isMatch(LambdaRouteAttribute lambdaRoute, string path, IDictionary<string, string> pathParameters)
	{
		var resourceCheck = lambdaRoute.Resource;
		var matchString = resourceCheck;
		
		matchString = pathParameters.Aggregate(lambdaRoute.Resource, (current, param) => current.Replace("{" + param.Key + "}", param.Value));

		matchString.Dump();
		path.Dump();
		(matchString == path).Dump();

		Console.WriteLine();
		Console.WriteLine();

		return matchString == path;
	}
}

public class LambdaRouteAttribute : Attribute
{
	public string Resource { get; }
	public IEnumerable<string> RouteParameters { get; private set; }

	public LambdaRouteAttribute(string resource)
	{
		Resource = resource;
		BuildParameters();
	}

	private void BuildParameters()
	{
		RouteParameters = Resource.Split(new[] { '/', }, StringSplitOptions.RemoveEmptyEntries);
//							.Where(x => x.Length >= 2 && x[0] == '{' && x[x.Length - 1] == '}')
//							.Select(x => x.TrimStart('{').TrimEnd('}'));
	}
}

