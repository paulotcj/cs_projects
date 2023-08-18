

var evt = new ManualResetEventSlim();

var t1 = Task.Factory.StartNew(() => {
    Console.WriteLine($"Boiling water");
    evt.Set();
});

var t2 = Task.Factory.StartNew(() => {
    Console.WriteLine($"Waiting for the water to boil....");
    evt.Wait();
    Console.WriteLine($"Here's your tea");
    evt.Wait();

});

t2.Wait();
Console.WriteLine("-------------------");


var evt2 = new AutoResetEvent(false);

t1 = Task.Factory.StartNew(() => {
    Console.WriteLine($"Boiling water");
    evt2.Set(); //true
});

t2 = Task.Factory.StartNew(() => {
    Console.WriteLine($"Waiting for the water to boil....");
    evt2.WaitOne(); //false
    Console.WriteLine($"Here's your tea");
    var ok = evt2.WaitOne(1000); //false

    if (ok) { Console.WriteLine($"Enjoy your tea."); }
    else { Console.WriteLine($"Tea crashed at: 0x129823234 - physics stopped working, blackhole starting..."); }


});

t2.Wait();


Console.WriteLine("-------------------");


Console.WriteLine("All Done");
