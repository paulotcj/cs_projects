
Console.WriteLine($"In this example both tasks are detached");
var t1 = Task.Factory.StartNew(() => {
    Console.WriteLine($"T1 task  - start");

    //detached 
    var t2 = Task.Factory.StartNew(() =>
    {
        Console.WriteLine($"T2 task  - start");
        Thread.Sleep( 1000 );
        Console.WriteLine($"T2 task  - end");
    });
    Console.WriteLine($"T1 task  - end");
});


Console.ReadKey();


Console.WriteLine($"-------------------------------");
Console.WriteLine($"In this example both tasks are detached, and t1 will likely finish before t2 had a chance to finish");
t1 = new Task( () => 
{ 
    Console.WriteLine($"T1 task  - start");

    //detached 
    var t2 = new Task(() =>
    {
        Console.WriteLine($"T2 task  - start");
        Thread.Sleep(1000);
        Console.WriteLine($"T2 task  - end");
    });
    t2.Start();
    //Thread.Sleep(1000);
    Console.WriteLine($"T1 task  - end");
});
t1.Start();

try { 
    t1.Wait();
    Console.WriteLine($"** t1.Wait();");
}
catch (AggregateException ae) {
    ae.Handle(e => true);
}


Console.ReadKey();



Console.WriteLine($"-------------------------------");
Console.WriteLine($"In this example both tasks are attached. T1 will start, then t2 will start and fininsh, and only then t1 will finish");
t1 = new Task(() =>
{
    Console.WriteLine($"T1 task  - start");

    //t2 is attached to t1 
    var t2 = new Task(() =>
    {
        Console.WriteLine($"T2 task, ID: {Task.CurrentId} - start");
        Thread.Sleep(1000);
        Console.WriteLine($"T2 task, ID: {Task.CurrentId} - end");
    }, TaskCreationOptions.AttachedToParent);

    var completion_handler = t2.ContinueWith(t =>
    {
        Console.WriteLine($"Finished Task ID: {t.Id} state is {t.Status}");
    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

    var fail_handler = t2.ContinueWith(t => {
        Console.WriteLine($"Fail Task ID: {t.Id} state is {t.Status}");
    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

    t2.Start();
    //Thread.Sleep(1000);
    Console.WriteLine($"T1 task  - end");
});
t1.Start();

try
{
    t1.Wait();
    Console.WriteLine($"** t1.Wait();");
}
catch (AggregateException ae)
{
    ae.Handle(e => true);
}


Console.ReadKey();



Console.WriteLine($"-------------------------------");
Console.WriteLine($"In this example both tasks are attached. But T2 will cause an error");
t1 = new Task(() =>
{
    Console.WriteLine($"T1 task  - start");

    //t2 is attached to t1 
    var t2 = new Task(() =>
    {
        Console.WriteLine($"T2 task, ID: {Task.CurrentId} - start");
        Thread.Sleep(1000);
        throw new Exception("Something random");
        Console.WriteLine($"T2 task, ID: {Task.CurrentId} - end");
    }, TaskCreationOptions.AttachedToParent);

    var completion_handler = t2.ContinueWith(t =>
    {
        Console.WriteLine($"Finished Task ID: {t.Id} state is {t.Status}");
    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

    var fail_handler = t2.ContinueWith(t => {
        Console.WriteLine($"Fail Task ID: {t.Id} state is {t.Status}");
    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

    t2.Start();
    //Thread.Sleep(1000);
    Console.WriteLine($"T1 task  - end");
});
t1.Start();

try
{
    t1.Wait();
    Console.WriteLine($"** t1.Wait();");
}
catch (AggregateException ae)
{
    ae.Handle(e => true);
    Console.WriteLine($"** Exception handler - T2 is expeted to fail");
}


Console.ReadKey();





Console.WriteLine($"All done");