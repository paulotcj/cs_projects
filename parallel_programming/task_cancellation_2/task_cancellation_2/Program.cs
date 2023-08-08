
var planned = new CancellationTokenSource();
var preventive = new CancellationTokenSource();
var emergency = new CancellationTokenSource();

var paranoid = CancellationTokenSource.CreateLinkedTokenSource(planned.Token, preventive.Token, emergency.Token);

Task.Factory.StartNew(() => 
{
    int i = 0;
    while (true)
    { 
        paranoid.Token.ThrowIfCancellationRequested();
        Console.WriteLine(i++);
        Thread.Sleep(1000);
    }
},paranoid.Token);
Console.ReadKey();
emergency.Cancel();



Console.WriteLine("Done.");
Console.ReadKey();