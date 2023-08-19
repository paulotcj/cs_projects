

var numbers = Enumerable.Range(1,20).ToArray();

var results = numbers
    .AsParallel()
    .WithMergeOptions(ParallelMergeOptions.NotBuffered) //items produced will be consumed as soon as they are available
    //.WithMergeOptions(ParallelMergeOptions.FullyBuffered) //items produced will and only then consumed
    .Select(x =>
{
    var result = Math.Log10(x);
    Console.WriteLine($"++  x: {x}, result: {result} - produced");
    return result;
});

//results -> ParallelQuery<double>? results
foreach (var result in results) {
    Console.WriteLine($"--  result:{result} - consumed");
}





Console.WriteLine($"-----------------------------------------");
Console.WriteLine("done");
