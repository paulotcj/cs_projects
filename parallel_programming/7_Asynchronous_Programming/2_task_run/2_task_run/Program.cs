
//Task.Run equivalent to:
var a = Task.Factory.StartNew(() => {
        Console.WriteLine("Inside the task");
    },
    CancellationToken.None,
    TaskCreationOptions.DenyChildAttach,
    TaskScheduler.Default);

Task.WaitAll(a); //not this though, this was only put in here so the examples were better organized

Console.WriteLine($"----------------------------");
// b is of type Task<Task<int>>? b
var b = Task.Factory.StartNew(() => {
    Console.WriteLine($"Inside the Outer Task");
    var inner_task = Task.Factory.StartNew(() => { 
        Console.WriteLine($"Inside the Inner Task");
        return 123;
    });
    return inner_task;
});
Console.WriteLine($"B: {b}");
//This is type Task<int>? result_of_b
var result_of_b = b.Result;
Console.WriteLine($"result_of_b: {result_of_b}");
var result_of_result_of_b = result_of_b.Result;
Console.WriteLine($"result_of_result_of_b: {result_of_result_of_b}");


Console.WriteLine($"----------------------------");
// c is of type Task<Task<int>>? c
var c = Task.Factory.StartNew(async () => { 
    await Task.Delay(1000);
    return 123;
} );
Console.WriteLine($"c: {c}");

Console.WriteLine($"----------------------------");
// d is of type Task<int>? d
var d = Task.Factory.StartNew(async () => {
    await Task.Delay(1000);
    return 123;
}).Unwrap();
Console.WriteLine($"d: {d}");

Console.WriteLine($"----------------------------");
// e is of type int
var e = await await Task.Factory.StartNew(async () =>
{
    await Task.Delay(1000);
    return 123;
});
Console.WriteLine($"e: {e}");

Console.WriteLine($"----------------------------");
// f is of Task<int>
var f = await Task.Factory.StartNew(async delegate 
{ 
    await Task.Delay(1000);
    return 123;
});
Console.WriteLine($"f: {f}");




Console.WriteLine($"----------------------------");
Console.WriteLine("Done");
