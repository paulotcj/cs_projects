
const int count = 50;
var items = Enumerable.Range(1, count).ToArray();
var results = new int[count];

items.AsParallel().ForAll(x => {
    int new_value = x * x * x;
    Console.WriteLine($"x:{x}, x^3: {new_value} - Task ID: {Task.CurrentId}");
    results[x-1] = new_value;

});

Console.WriteLine($"---------------------");

var cubes = items.AsParallel().Select(x => x);

foreach (var item in cubes) { Console.WriteLine($"Item: {item}"); }



Console.WriteLine($"---------------------");

cubes = items.AsParallel().AsOrdered().Select(x => x);
foreach (var item in cubes) { Console.WriteLine($"Item: {item}"); }



Console.WriteLine($"---------------------");
Console.WriteLine("Done");
