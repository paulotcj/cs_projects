
var sum = Enumerable.Range(1,1000).Sum(x => x);
Console.WriteLine($"Enumerable.Range(1,1000).Sum(x => x): {sum}");

Console.WriteLine($"--------------------------");
var average = Enumerable.Range(1, 1000).Average(x => x);
Console.WriteLine($"Enumerable.Range(1, 1000).Average(x => x): {average}");


Console.WriteLine($"--------------------------");
var sum2 = Enumerable.Range(1, 1000).Aggregate(0, (i,accumulator) => i+ accumulator);
Console.WriteLine($"Enumerable.Range(1, 1000).Aggregate(0, (i,accumulator) => i+ accumulator): {sum2}");


Console.WriteLine($"--------------------------");
var sum3 = ParallelEnumerable.Range(1, 1000)
    .Aggregate(
    0,
    (partialsum, i) => partialsum += i,
    (total, subtotal) => subtotal += subtotal,
    i => i
    );
Console.WriteLine($"sum3: {sum3}");


Console.WriteLine($"--------------------------");
Console.WriteLine("Done");
