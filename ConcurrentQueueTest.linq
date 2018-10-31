<Query Kind="Program">
  <NuGetReference>Polly</NuGetReference>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Polly</Namespace>
  <Namespace>Polly.CircuitBreaker</Namespace>
  <Namespace>Polly.Retry</Namespace>
</Query>

void Main()
{
	var testStrings = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
	var testConnectionPool = new QueueManagerConnectionPool(testStrings);

	var timer = new Stopwatch();
	timer.Start();

	Parallel.For(0, 100000, index =>
	{
		Console.WriteLine($"Starting {index}...");
		if(timer.ElapsedMilliseconds > 15000) return;
		var result = testConnectionPool.SendAndGet();
		Console.WriteLine($"      Success! Received {result}.");
	});
	
	timer.Stop();
	testConnectionPool.DumpMe();
}

private class QueueManagerConnectionPool
{
	private static Random _rnd = new Random();	
	public static RetryPolicy MyRetryPolicy = Policy.Handle<MyException>().WaitAndRetryForever(x => TimeSpan.FromSeconds(1));
	
	public static ConcurrentQueue<KeyValuePair<string, MyConnectionPoolData>> ConnectionPool = new ConcurrentQueue<System.Collections.Generic.KeyValuePair<string, MyConnectionPoolData>>();

	public QueueManagerConnectionPool(string[] initStrings) {
		Parallel.ForEach(initStrings, (str) => {
			var policy = Policy.Handle<MyException>().CircuitBreaker(2, TimeSpan.FromSeconds(10));
			var myData = new MyConnectionPoolData(policy);
			ConnectionPool.Enqueue(new KeyValuePair<string, MyConnectionPoolData>(str, myData));
		});
	}

	public string SendAndGet() {	
		var result = MyRetryPolicy.Execute(CircuitBreakerHandler);
		Console.WriteLine($"    Returning {result} from SendAndGet");
		return result;
	}

	private string CircuitBreakerHandler()
	{
		KeyValuePair<string, MyConnectionPoolData> kvp;
		var success = ConnectionPool.TryDequeue(out kvp);

		if (!success) return string.Empty;

		var myData = kvp.Value;

		if (myData.CBPolicy.CircuitState == CircuitState.Closed)
		{
			var result = myData.CBPolicy.Execute(() =>
			{
				Console.WriteLine($"Executing attempt #{myData.TotalCount} for {kvp.Key}");

				try
				{
					return ErrorAtRandom();
				}
				catch (MyException ex)
				{
					myData.ErrorCount = kvp.Value.ErrorCount + 1;
					Console.WriteLine($"  Threw {ex.ErrorNumber} for {kvp.Key} on attempt #{myData.TotalCount}");
					throw;
				}
				finally
				{
					myData.TotalCount = kvp.Value.TotalCount + 1;
					var newKvp = new KeyValuePair<string, MyConnectionPoolData>(kvp.Key, myData);
					ConnectionPool.Enqueue(newKvp);
				}
			});
			
			return result;
		}
		else
		{
			Console.WriteLine($"Circuit Breaker OPEN for {kvp.Key} on attempt #{myData.TotalCount}. Resetting.");
			myData.TotalCount = kvp.Value.TotalCount + 1;
			myData.ResetCount = kvp.Value.ResetCount + 1;
			myData.CBPolicy.Reset();
			var newKvp = new KeyValuePair<string, MyConnectionPoolData>(kvp.Key, myData);
			ConnectionPool.Enqueue(newKvp);
		}
		
		return string.Empty;
	}

	private string ErrorAtRandom()
	{
		var error = _rnd.Next(1, 20000);
		if (error > 3000 && error < 5000) throw new MyException(error);
		else if (error < 1000 || error > 19000) Task.Delay(TimeSpan.FromSeconds(2));
		Console.WriteLine($"  Returning {error} from ErrorAtRandom");
		return error.ToString();
	}

	public void DumpMe()
	{
		Parallel.ForEach(ConnectionPool, (kvp) => {		
			kvp.Dump();
		});
	}
}

public class MyConnectionPoolData
{
	public CircuitBreakerPolicy CBPolicy {get; set;}
	public int TotalCount {get; set;}
	public int ErrorCount {get; set;}
	public int ResetCount {get; set;}
	
	public MyConnectionPoolData(CircuitBreakerPolicy policy) {
		this.CBPolicy = policy;
		this.TotalCount = 0;
		this.ErrorCount = 0;
		this.ResetCount = 0;
	}	
}

public class MyException : Exception {
	public int ErrorNumber {get; set;}
	
	public MyException(int num) {
		this.ErrorNumber = num;
	}
}