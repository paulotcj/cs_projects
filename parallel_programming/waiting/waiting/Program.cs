var t1 = new Task(() => { Thread.Sleep(1000); Console.WriteLine("T1 : Finished sleeping"); });
t1.Start();


var t2 = new Task(() => { Thread.SpinWait(1000) ; Console.WriteLine("T2: Finished sleeping"); });
t2.Start();

var t3 = new Task(() => {
    SpinWait.SpinUntil(() =>
    {
        return false;
    }
    , new TimeSpan(0, 0, 0, 0, 1000));
    Console.WriteLine("T3: Finished sleeping"); 
});
t3.Start();

//----------------
var cts4 = new CancellationTokenSource();
var token4 = cts4.Token;
var t4 = new Task(() => { 
    Console.WriteLine("T4: You have 5 seconds to cancel this task before it's done."); 
    bool cancelled = token4.WaitHandle.WaitOne(5000);
    Console.WriteLine(cancelled ? "T4: Task Cancelled" : "T4: You allowed this task to run until the its end.");
},token4);
t4.Start();
Console.ReadKey();
cts4.Cancel();

Console.WriteLine("All Done");
Console.ReadKey();
