
int sum = 0;
Parallel.For(fromInclusive: 1, toExclusive: 1001, body: (x) => { 
    Interlocked.Add(ref sum, x);
});
Console.WriteLine($"Parallel.For - Using Interlocked.Add() result: {sum}");
Console.WriteLine($"------------------------------------");

sum = 0;

Parallel.For(fromInclusive: 1, toExclusive: 1001,
    () => 0,
    (x, state, tls) =>
    {
        var tls_sum = tls + x;
        Console.WriteLine($"    Task ID: {Task.CurrentId} - tls: {tls}, x: {x}, tls_sum = {tls_sum}");
        tls = tls_sum;
        return tls;
    },
    partialSum => {
        Interlocked.Add(ref sum, partialSum);
    }
);
Console.WriteLine($"Parallel.For - Using PartialSum result: {sum}");



Console.WriteLine($"------------------------------------");
Console.WriteLine("All done");
