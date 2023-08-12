
var t1 = Task.Factory.StartNew(() => { 
    throw new InvalidOperationException("Can't do this") { Source = "T1" };
});

var t2 = Task.Factory.StartNew(() => {
    throw new AccessViolationException("Can't access this") { Source = "T2" };
});


try
{
    Task.WaitAll(t1, t2);
}
catch (AggregateException ae)
{
    foreach (Exception e in ae.InnerExceptions)
    {
        Console.WriteLine($"Exception {e.GetType()} from {e.Source}.");
    }
}

Console.WriteLine("All Done");
Console.ReadKey();