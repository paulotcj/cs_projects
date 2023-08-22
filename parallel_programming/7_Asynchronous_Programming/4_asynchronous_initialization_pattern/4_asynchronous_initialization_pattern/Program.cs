
var a = new MyClass();
if (a is IAsyncInit ai) { 
    await ai.InitTask;
    Console.WriteLine($"a is IAsyncInit - await InitTask (which is 'InitTask = InitAsync()' )");
}

Console.WriteLine($"------------------------------------");
Console.WriteLine("Done");

public interface IAsyncInit { Task InitTask { get; } }

public class MyClass : IAsyncInit {
    public MyClass() {
        InitTask = InitAsync();
    }
    public Task InitTask { get; }
    private async Task InitAsync() { 
        await Task.Delay( 1000 );
    }
}

