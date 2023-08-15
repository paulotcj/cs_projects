

using System.Collections.Concurrent;

var b = new ConcurrentBag<int>(); //No ordering

var tasks = new List<Task>();

for (int i = 0; i < 10; i++)
{
    var i1 = i;
    tasks.Add( Task.Factory.StartNew(() => {
        b.Add(i1);
        Console.WriteLine($"TaskID: {Task.CurrentId} has added: {i1}");
        int result;
        if (b.TryPeek(out result)) 
        {
            Console.WriteLine($"TaskID: {Task.CurrentId} is peeking the bag with result: {result}");
        }
    }));
}

Task.WaitAll(tasks.ToArray());


Console.WriteLine("------------------------------------------------------------");


b = new ConcurrentBag<int>(); //No ordering

tasks = new List<Task>();

for (int i = 0; i < 10; i++)
{
    var i1 = i;
    tasks.Add(Task.Factory.StartNew(() => {

        if (i1 % 2 == 0)
        {
            b.Add(i1);
            Console.WriteLine($"TaskID: {Task.CurrentId} has added: {i1}");
        }
        else
        {
            int result;
            if (b.TryPeek(out result))
            {
                Console.WriteLine($"TaskID: {Task.CurrentId} is peeking the bag with result: {result}");
            }
        }


    }));
}

Task.WaitAll(tasks.ToArray());

Console.WriteLine("------------------------------------------------------------");


b = new ConcurrentBag<int>(); //No ordering

tasks = new List<Task>();

for (int i = 0; i < 20; i++)
{
    var i1 = i;
    tasks.Add(Task.Factory.StartNew(() => {

        if (i1 % 2 == 0)
        {
            b.Add(i1);
            Console.WriteLine($"TaskID: {Task.CurrentId} has added: {i1}");
        }
        else
        {
            int result;
            if (b.TryTake(out result))
            {
                Console.WriteLine($"TaskID: {Task.CurrentId} is TryTake the bag with result: {result}");
            }
        }


    }));
}

Task.WaitAll(tasks.ToArray());


Console.WriteLine("Done");
