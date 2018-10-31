<Query Kind="Statements" />

var resource = "arn:aws:execute-api:us-west-2:768833718104:ncsdpp9or0/";
var resourceTokens = resource.Split(':');
var lastIdx = resourceTokens.Length - 1;
var outerLeaf = resourceTokens[lastIdx];

if (outerLeaf != null)
{
	var idx = outerLeaf.LastIndexOf("/", StringComparison.Ordinal);
	idx.Dump();
	if (idx > -1) resourceTokens[lastIdx] = string.Concat(outerLeaf.Substring(0, idx), "/*");
}

var finalResource = string.Join(":", resourceTokens);
finalResource.Dump();