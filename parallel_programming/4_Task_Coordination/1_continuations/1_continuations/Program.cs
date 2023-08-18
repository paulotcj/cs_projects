
using System.Collections.Concurrent;

var t1 = Task.Factory.StartNew(() => {

    Console.WriteLine($"Taskid: {Task.CurrentId} - Boiling Water");
});

var t2 = t1.ContinueWith(t => {
    Console.WriteLine($"Taskid: {Task.CurrentId} - Completed task {t.Id} - pour water into the cup.");
});

t2.Wait();

Console.WriteLine("---------------------------");
var t3 = Task.Factory.StartNew(() => "t3");
var t4 = Task.Factory.StartNew(() => "t4");
var t5 = Task.Factory.ContinueWhenAll(new[] { t3, t4 }, tasks => {
    Console.WriteLine($"Tasks Completed");
    foreach (var task in tasks) { 
        Console.WriteLine($" - {task.Result}");
    }
    Console.WriteLine($"All tasks done.");
});





Console.WriteLine("Done");
