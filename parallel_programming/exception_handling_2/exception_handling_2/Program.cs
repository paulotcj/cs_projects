
try 
{
    DummyFunc();
}
catch (AggregateException ae) 
{
    foreach (var e in ae.InnerExceptions) 
    { 
        Console.WriteLine($"Handled at the initial try/catch at Main. Exception Type: {e.GetType()} - Expected: AccessViolation");
    }
}

Console.WriteLine("All Done");
Console.ReadKey();

static void DummyFunc()
{
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
        ae.Handle(e => {
            if (e is InvalidOperationException)
            {
                Console.WriteLine("Invalid Operation - Handled inside DummyFunc()");
                return true;
            }
            else { return false; }
        });

    }
}