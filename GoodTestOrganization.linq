<Query Kind="Program" />

void Main()
{
	// ***************
	// Unit Tests
	// ***************
	
	// One adds two and three
	var one = new One();
	var result1 = one.AddTwoThree();
	Console.WriteLine($"{result1 == 5}");
	
	// Two doubles three
	var two = new Two();
	var result2 = two.DoubleThree();
	Console.WriteLine($"{result2 == 6}");

	// Three adds a positive number of times
	var three = new Three();
	var result3 = three.AddOne(3);
	Console.WriteLine($"{result3 == 15}");

	// Three cannot add a negative number of times
	three = new Three();
	var result4 = three.AddOne(-1);
	Console.WriteLine($"{result3 == 0}");

	// Integration Tests
	
}

public class One 
{
	public static Random Rnd = new Random();	
	
	public Two Two => new Two();
	public Three Three => new Three();
	public int Value
	{
		get
		{
			// Mimick some unknown failure
			if (Rnd.Next(1, 5) == 3) throw new Exception("Haha from One!");
			return 1;
		}
		set { }
	}
	
	public int AddTwoThree() 
	{
		return Two.Value + Three.Value;
	}
}

public class Two
{
	static int Cntr;
	public Three Three => new Three();
	public int Value => 2;

	public int DoubleThree()
	{
		Cntr++;
		
		// Mimick some unknown failure
		if (Cntr == 3) throw new Exception("Haha from Two!");
		
		return Three.Value * 2;
	}
}

public class Three
{	
	public One One => new One();
	public int Value => 3;

	public int AddOne(int numberOfTimes)
	{
		int result = 0;
		for(var i = 0; i < numberOfTimes; i++) 
		{
			result += One.AddTwoThree();
		}	
		
		// Mimick some unknown failure
		if(numberOfTimes == 1) throw new Exception("Haha from Three!");
		
		return result;
	}
}

public class Four 
{ 
	public One One => new One();
	public Two Two => new Two();
	public Three Three => new Three();
	
	public int AddOneTwoThree() {
		return 1;
	}	
}