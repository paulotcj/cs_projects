
using System.Collections.Concurrent;

var s = new ConcurrentStack<int>();



s.Push(1);
s.Push(2);
s.Push(3);
s.Push(4);

int result;
if (s.TryPeek(out result)) 
{
    Console.WriteLine($"TryPeek result is: {result}");  
}
if (s.TryPop(out result))
{
    Console.WriteLine($"TryPop result is: {result}");
}

var items = new int[5];
if (s.TryPopRange(items,0,5) > 0) //result is how many elements you actually was able to pop
{
    Console.WriteLine($"TryPop result is: { string.Join(", ", items)  }");
}



//for (int i = 1; i < 10; i++) { s.Push(i); }


Console.WriteLine("Done");
