

Console.WriteLine($"-----------------------------");
Console.WriteLine("Done");


public class Stuff {
    private static int value;
    private readonly Lazy<Task<int>> AutoIncValue = new Lazy<Task<int>> (async () => {
        await Task.Delay (1000).ConfigureAwait (false);
        return value++;
    });
    public async Task UseValue() { 
        int value = await AutoIncValue.Value;
    }
}