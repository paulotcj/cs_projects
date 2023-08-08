


//------
var cts1 = new CancellationTokenSource();
var token1 = cts1.Token;

token1.Register(callback: () => { Console.WriteLine("Cancellation has been requested"); } );

var t1 = new Task(action: () => 
{
    int i = 0;
    while (true)
    {
        ////method 1
        //if (token1.IsCancellationRequested)
        //{ break; }

        ////method 2
        //// note: this does not actually throw an exception to the main program
        //if (token1.IsCancellationRequested) { throw new OperationCanceledException(); }

        //method 3
        token1.ThrowIfCancellationRequested();

        Console.WriteLine(i++);
    }
}, cancellationToken: token1);
t1.Start();

Console.WriteLine("\n\nStarting a new task: T2");
Task.Factory.StartNew(() =>
{
    token1.WaitHandle.WaitOne();
    Console.WriteLine("Wait handle released, cancellation was requested");
});

Console.ReadKey();
cts1.Cancel();
//------



Console.WriteLine("Done.");
Console.ReadKey();