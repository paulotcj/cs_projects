var barrier = new Barrier(2, b=> {
    Console.WriteLine($"    Phase: {b.CurrentPhaseNumber} is finished");
});

Console.WriteLine($"Note: Taks executing in between phases, run in parallel.");
Console.WriteLine($"  You will likely see step no. 2 finishing before step 1 because step 2 is faster");
Console.WriteLine($"  But the phases are coordinated. So you might see: 2,1 -> 3,4 -> 6,5 -> 8,7 -> 9,10 ...");
Console.WriteLine($"----------");


var water = Task.Factory.StartNew(Water);
var cup = Task.Factory.StartNew(Cup);


var tea = Task.Factory.ContinueWhenAll(new[] {water, cup }, tasks => {

    Console.WriteLine($"----------");
    Console.WriteLine($"Enjoy your cup of tea");
});

tea.Wait();


Console.WriteLine("All Done");



void Water() {
    Console.WriteLine($" 1 - Putiting the kettle on - it takes a bit of time");
    Thread.Sleep(1000);
    barrier.SignalAndWait();
    Console.WriteLine($" 3 - Pouring water into the cup");
    barrier.SignalAndWait();
    Console.WriteLine($" 5 - Putting the kettle away");
    barrier.SignalAndWait();
    Console.WriteLine($" 7 - Doing something...");
    barrier.SignalAndWait();
    Console.WriteLine($" 9 - Doing something [2]...");
}

void Cup()
{
    Console.WriteLine($" 2 - Finding a cup of tea - fast");
    //Thread.Sleep(1000);
    barrier.SignalAndWait();
    Console.WriteLine($" 4 - Adding tea");
    barrier.SignalAndWait();
    Console.WriteLine($" 6 - Adding sugar");
    barrier.SignalAndWait();
    Console.WriteLine($" 8 - Doing something else...");
    barrier.SignalAndWait();
    Console.WriteLine($"10 - Doing something else [2]...");
}