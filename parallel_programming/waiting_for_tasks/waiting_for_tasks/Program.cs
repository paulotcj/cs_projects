
var cts1 = new CancellationTokenSource();
var token1 = cts1.Token;

var t1 = new Task(() => 
{
    Console.WriteLine("T1: I take 5 seconds.");
    for (int i = 0; i < 5; i++)
    { 
        token1.ThrowIfCancellationRequested();
        Thread.Sleep(1000);
    }
    Console.WriteLine("T1: Is done");
},token1);
t1.Start();

Task t2 = Task.Factory.StartNew(() => { Console.WriteLine("T2: Start"); Thread.Sleep(2000); Console.WriteLine("T2: End"); }, token1);

//Method 1
//Task.WaitAll(t1, t2);

//method 2
//t1.Wait(token1);

//method 3
//Task.WaitAny(t1, t2);


//method 4
//Task.WaitAny(tasks: new[] { t1, t2 } , millisecondsTimeout: 4000, cancellationToken: token1   );

//method 5
Task.WaitAll(tasks: new[] { t1, t2 }, millisecondsTimeout: 3000, cancellationToken: token1);

Console.WriteLine($"T1 status: {t1.Status}");
Console.WriteLine($"T2 status: {t2.Status}");


Console.WriteLine("All Done");
