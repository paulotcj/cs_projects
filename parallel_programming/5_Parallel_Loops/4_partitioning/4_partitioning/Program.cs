
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;

Console.WriteLine("Hello, World!");

//var summary = BenchmarkRunner.Run<>

var summary = BenchmarkRunner.Run<zzz>();
Console.WriteLine(summary);

public class zzz {
    [Benchmark]
    public void SquareEachValue()
    {
        const int count = 100_000;
        var values = Enumerable.Range(0, count);
        var results = new int[count];
        Parallel.ForEach(values, x =>
        {
            results[x] = (int)Math.Pow(x, 2);
        });
    }

    [Benchmark]
    public void SquareEachValueChunked()
    {
        const int count = 100_000;
        var values = Enumerable.Range(0, count);
        var results = new int[count];

        var part = Partitioner.Create(0, count, 10_000);
        Parallel.ForEach(part, range =>
        {
            for (int i = range.Item1; i < range.Item2; i++)
            {
                results[i] = (int)Math.Pow(i, 2);
            }
        });


    }
}
