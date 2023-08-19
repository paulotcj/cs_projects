var cts = new CancellationTokenSource();


var items = ParallelEnumerable.Range(1, 20);
var results = items.WithCancellation(cts.Token)
    .Select(i =>
{
    double result = Math.Log10(i);

    //if (result > 1) { throw new InvalidOperationException("Custom Error"); }
    if (result > 1) { cts.Cancel();  }

    Console.WriteLine($"i: {i}, result: {result}, task id: {Task.CurrentId}");
    return result;
});

try {
    foreach (var c in results)
    { 
        Console.WriteLine($"result: {c}");
    }
}
catch (AggregateException ae) 
{
    ae.Handle(e => {
        Console.WriteLine($"Exception: {e.GetType().Name} - Message: {e.Message}");
        return true;
    });
}
catch (OperationCanceledException e) {
    Console.WriteLine($"Operation Canceled: {e.Message}");
}


Console.WriteLine($"--------------------------------");
Console.WriteLine("Done");
