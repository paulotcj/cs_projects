
Console.WriteLine("Press a key - This will call a blocking operation");
Console.ReadKey();
Console.WriteLine($"    1 - Calling the Async method");
int result_blocking = CalculateValue();
Console.WriteLine($"    2 - Result from the Async call: {result_blocking}");
Console.WriteLine($"    3 - Line below the result of Async");



Console.WriteLine($"--------------------------------");
Console.WriteLine("Press a key - This will call a non-blocking operation");
Console.ReadKey();

Console.WriteLine($"Calling the Async method");
var resultAsync = CalculateValueAsync();
var resultAsync_2 = resultAsync.ContinueWith(t => {
    Console.WriteLine($"    ? - Inside the Async Call");
    return t.Result;
});

Console.WriteLine($"    1 - This is a line immediately below the async call, this should not be blocked\nPress a key");
Console.WriteLine($"    2 - Result from the Async call: {resultAsync_2.Result}");
Console.WriteLine($"    3 - Line below the result of Async");
Console.ReadKey();



Console.WriteLine($"--------------------------------");
Console.WriteLine("Press a key - This will call a non-blocking operation - Using AWAIT");
Console.ReadKey();

Console.WriteLine($"Calling the Async method");
var result_int = await CalculateValueAsync_2();

Console.WriteLine($"    1 - This is a line immediately below the async call. This will be blocked because we await the result\nPress a key");
Console.WriteLine($"    2 - Result from the Async call: {result_int}");
Console.WriteLine($"    3 - Line below the result of Async");
Console.ReadKey();


Console.WriteLine($"--------------------------------");
Console.WriteLine("Press a key - This will call a non-blocking operation - Using AWAIT");
Console.ReadKey();

Console.WriteLine($"Calling the Async method");
var result2 = CalculateValueAsync_2();

Console.WriteLine($"    1 - This is a line immediately below the async call. this should not be blocked\nPress a key");
Console.WriteLine($"    2 - Result from the Async call: {result2.Result}");
Console.WriteLine($"    3 - Line below the result of Async");
Console.ReadKey();


Console.WriteLine($"--------------------------------");
Console.WriteLine("Press a key - This will call a non-blocking operation - Using AWAIT");
Console.ReadKey();

Console.WriteLine($"Calling the Async method");
result2 = CalculateValueAsync_2();

Console.WriteLine($"    1 - This is a line immediately below the async call. this should not be blocked\nPress a key");
Console.WriteLine($"    2 - Result from the Async call using 'await result2': {await result2}");
Console.WriteLine($"    3 - Line below the result of Async");
Console.ReadKey();




Console.WriteLine($"--------------------------------");
Console.WriteLine("Done");


int CalculateValue() { 
    Thread.Sleep( 1000 );
    return 123;
}


Task<int> CalculateValueAsync()
{
    return Task.Factory.StartNew(() => {
        Thread.Sleep(1000);
        return 123;
    });
}


async Task<int> CalculateValueAsync_2()
{
    await Task.Delay(1000);
    Console.WriteLine($"Inside CalculateValueAsync_2");
    return 123;
}