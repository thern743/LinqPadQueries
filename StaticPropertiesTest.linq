<Query Kind="Program" />

void Main()
{
	var test = new TestClass();
	test.Execute();	
	test.Execute();

	var test2 = new TestClass();
	test.Execute();
	test.Execute();
}

public class TestClass 
{
	private static string MyTestStaicString1 => TestMethod();

	private static string MyTestStaicString2 => $"Yo2_{Counter2}";
	
	private static string _MyTestStaicString3;
	private static string MyTestStaicString3 { get { Counter3++; return _MyTestStaicString3; } set { _MyTestStaicString3 = value; } }
	
	private static int Counter1;
	private static int Counter2;
	private static int Counter3;
	
	public TestClass() {
		MyTestStaicString3 = $"Yo3_{Counter3}";
	}

	public void Execute()
	{
		Console.WriteLine($"First call MyTestStaicString1 {MyTestStaicString1}");
		Console.WriteLine($"First call MyTestStaicString2 {MyTestStaicString2}");
		Console.WriteLine($"First call MyTestStaicString3 {MyTestStaicString3}");
		
		Console.WriteLine($"Second call MyTestStaicString1 {MyTestStaicString1}");
		Console.WriteLine($"Second call MyTestStaicString2 {MyTestStaicString2}");
		Console.WriteLine($"Second call MyTestStaicString3 {MyTestStaicString3}");
		
		Console.WriteLine($"Third call MyTestStaicString1 {MyTestStaicString1}");
		Console.WriteLine($"Third call MyTestStaicString2 {MyTestStaicString2}");
		Console.WriteLine($"Third call MyTestStaicString3 {MyTestStaicString3}");
		
		Console.WriteLine($"Counter1 = {Counter1}");
		Console.WriteLine($"Counter2 = {Counter2}");
		Console.WriteLine($"Counter3 = {Counter3}");
	}
	
	private static string TestMethod() {
		Counter1++;
		Console.WriteLine(Counter1);
		return $"Yo1!_{Counter1}";
	}
}