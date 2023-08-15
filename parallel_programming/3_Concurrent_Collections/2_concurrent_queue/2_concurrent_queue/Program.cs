using System.Collections.Concurrent;

var q = new ConcurrentQueue<int>();
q.Enqueue(1);
q.Enqueue(2);

int result;

if (q.TryDequeue(out result)) 
{
    Console.WriteLine($"Removed element: {result}");
}

if (q.TryPeek(out result)) 
{
    Console.WriteLine($"Peek element: {result}");
}


Console.WriteLine("Done");
