<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	const int numberOfTask = 2;
	const int degreesOfParallelism = 4;

	var options = new ParallelOptions { MaxDegreeOfParallelism = degreesOfParallelism };

	var taskList1 = new Task[numberOfTask];

	var watch1 = new Stopwatch();
	watch1.Start();

	for (var i = 0; i < numberOfTask; i++)
	{
		var task = Task.Run(() =>
		{
			Parallel.For(0, 20, options, idx =>
			{
				Console.WriteLine($"Loop 1, Idx = {idx}, Thread ID = {Thread.CurrentThread.ManagedThreadId}, Task ID = {Task.CurrentId}");
				Task.Delay(1000);
			});
		});
		
		taskList1[i] = task;
	}

	Task.WaitAll(taskList1);

	watch1.Stop();
	
	Console.WriteLine();
	Console.WriteLine($"-----> Loop 1 Ran for {watch1.ElapsedMilliseconds}");
	Console.WriteLine();
	
	var taskList2 = new Task[numberOfTask];
	
	var watch2 = new Stopwatch();
	watch2.Start();

	for (var i = 0; i < numberOfTask; i++)
	{
		var task = Task.Run(() =>
		{
			Parallel.For(0, 20, options, idx =>
			{
				Console.WriteLine($"Loop 2, Idx = {idx}, Thread ID = {Thread.CurrentThread.ManagedThreadId}, Task ID = {Task.CurrentId}");
				Thread.Sleep(1000);
			});
		});
		
		taskList2[i] = task;
	}


	Task.WaitAll(taskList2);

	watch2.Stop();

	Console.WriteLine();
	Console.WriteLine($"-----> Loop 2 Ran for {watch2.ElapsedMilliseconds}");
	Console.WriteLine();
}