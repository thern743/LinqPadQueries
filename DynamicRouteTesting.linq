<Query Kind="Statements" />

string testRoute;
IEnumerable<string> routeAttr;

testRoute = "/credit/rewards/leveling/durable/{durable}/{action}/icon";
routeAttr = testRoute
				.Split(new[] { '/', }, StringSplitOptions.RemoveEmptyEntries)
				.Where(x => x.Length >= 2 && x[0] == '{' && x[x.Length - 1] == '}')
				.Select(x => x.TrimStart('{').TrimEnd('}'));
routeAttr.Dump();

var path1 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/upgrade/icon";
var pathParameters1 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "upgrade" }, { "level", "icon" }};
isMatch(pathParameters1).Dump();

Console.WriteLine();

testRoute = "/credit/rewards/leveling/durable/{durable}/{action}/five";
routeAttr = testRoute
				.Split(new[] { '/', }, StringSplitOptions.RemoveEmptyEntries)
				.Where(x => x.Length >= 2 && x[0] == '{' && x[x.Length - 1] == '}')
				.Select(x => x.TrimStart('{').TrimEnd('}'));
routeAttr.Dump();

var path2 = "/credit/rewards/leveling/durable/db5dc53d-f874-42d9-98cf-f5c55d5a64ee/upgrade/five";
var pathParameters2 = new Dictionary<string, string> { { "durable", "db5dc53d-f874-42d9-98cf-f5c55d5a64ee" }, { "action", "upgrade" }, { "level", "five" } };
isMatch(pathParameters2).Dump();

var parameters = new List<System.Collections.Generic.Dictionary<string, string>>();
parameters.Add(pathParameters1);
parameters.Add(pathParameters2);

var result = parameters.FirstOrDefault(x => isMatch(x)).Dump();

bool isMatch(Dictionary<string, string> parametersLocal)
{
	bool match = true;
	foreach(var value in routeAttr) {
		if(!parametersLocal.ContainsKey(value)) match = false;
	}
	return match;
}