

Demo1();
Demo2();
Demo3();
Demo4();


Console.WriteLine($"---------------------------------");
Console.WriteLine($"Done!");


void Demo1() {
    Console.WriteLine($"Starting 100 loops");
    Console.WriteLine($"---------------------------------");
    var result = Parallel.For(fromInclusive: 0, toExclusive: 100, body: (int x, ParallelLoopState state) => {
        Console.WriteLine($"x: {x} - Task ID: {Task.CurrentId}");
        if (x == 2) { 
            Console.WriteLine($"    stop() - action triggered\n      this will stop as soon AS POSSIBLE\n      meaning some tasks might continue to execute a little after this is triggered");
            state.Stop(); //this will stop as soon AS POSSIBLE
        }
    });

    Console.WriteLine($"Was this loop completed: {result.IsCompleted}");
    if (result.LowestBreakIteration.HasValue) { Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}"); }
    Console.WriteLine($"---------------------------------");
}


void Demo2()
{
    Console.WriteLine($"Starting 100 loops");
    Console.WriteLine($"---------------------------------");
    var result = Parallel.For(fromInclusive: 0, toExclusive: 100, body: (int x, ParallelLoopState state) => {
        Console.WriteLine($"x: {x} - Task ID: {Task.CurrentId}");
        if (x == 2)
        {
            Console.WriteLine($"    break() - action triggered\n      this will stop as soon AS POSSIBLE\n      meaning some tasks might continue to execute a little after this is triggered");
            state.Break(); //this will stop as soon AS POSSIBLE
        }
    });
    Console.WriteLine($"Was this loop completed: {result.IsCompleted}");
    if (result.LowestBreakIteration.HasValue) { Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}"); }
    Console.WriteLine($"---------------------------------");
}


void Demo3()
{
    Console.WriteLine($"Starting 100 loops");
    Console.WriteLine($"---------------------------------");
    var result = Parallel.For(fromInclusive: 0, toExclusive: 100, body: (int x, ParallelLoopState state) => {
        Console.WriteLine($"x: {x} - Task ID: {Task.CurrentId}");
        if (x == 2)
        {
            //Console.WriteLine($"    Exception - action triggered\n      this will stop as soon AS POSSIBLE\n      meaning some tasks might continue to execute a little after this is triggered");
            //throw new Exception("Generic exception");
        }
    });
    Console.WriteLine($"Was this loop completed: {result.IsCompleted}");
    if (result.LowestBreakIteration.HasValue) { Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}"); }
    Console.WriteLine($"---------------------------------");
}


void Demo4()
{
    var cts = new CancellationTokenSource();
    var po = new ParallelOptions();
    po.CancellationToken = cts.Token;

    Console.WriteLine($"Starting 100 loops");
    Console.WriteLine($"---------------------------------");
    var result = Parallel.For(fromInclusive: 0, toExclusive: 100,parallelOptions: po, body: (int x, ParallelLoopState state) => {
        Console.WriteLine($"x: {x} - Task ID: {Task.CurrentId}");
        if (x == 2)
        {
            Console.WriteLine($"    Cancellation token triggered");
            cts.Cancel();
        }
    });
    Console.WriteLine($"Was this loop completed: {result.IsCompleted}");
    if (result.LowestBreakIteration.HasValue) { Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}"); }
    Console.WriteLine($"---------------------------------");
}
