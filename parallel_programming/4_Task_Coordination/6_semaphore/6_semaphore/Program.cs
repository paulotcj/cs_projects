

var semaphore = new SemaphoreSlim(initialCount: 2, maxCount: 10);

for (int i = 0; i < 20; i++)
{
    Task.Factory.StartNew(() => 
    {
        Console.WriteLine($"      Entering task: {Task.CurrentId}");
        semaphore.Wait(); // ReleaseCount--
        Console.WriteLine($"  Processing task: {Task.CurrentId}");
    });
}

while (semaphore.CurrentCount <= 2) 
{
    Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
    Console.ReadKey();
    semaphore.Release(2);

}


Console.WriteLine("Done");
