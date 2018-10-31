<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
</Query>

void Main()
{
	object test = "true";
	var result = (test is bool && (bool)test) || test is HttpContent || test is string;
	result.Dump();

	test = true;
	result = (test is bool && (bool)test)  || test is HttpContent || test is string;
	result.Dump();

	test = false;
	result = (test is bool && (bool)test)  || test is HttpContent || test is string;
	result.Dump();

	test = "false";
	result = (test is bool && (bool)test)  || test is HttpContent || test is string;
	result.Dump();

	test = new StringContent("this is a test");
	result = (test is bool && (bool)test)  || test is HttpContent || test is string;
	result.Dump();
}
