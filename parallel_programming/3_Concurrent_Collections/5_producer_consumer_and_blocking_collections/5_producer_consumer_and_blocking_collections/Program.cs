using System.Collections.Concurrent;

var messages = new BlockingCollection<int>( new ConcurrentBag<int>(), 10  );
var cts = new CancellationTokenSource();
var random = new Random();

Task.Factory.StartNew(start_producer_and_consumer, cts.Token  );
Console.ReadKey();
cts.Cancel();




Console.WriteLine("Done");

void start_producer_and_consumer()
{
    var producer = Task.Factory.StartNew(RunProducer);
    var consumer = Task.Factory.StartNew(RunConsumer);


    try
    {
        Task.WaitAll(new[] { producer, consumer }, cts.Token);
    }
    catch (AggregateException ae)
    {
        ae.Handle(e => true);
    }
}

void RunConsumer()
{
    foreach (var message in messages.GetConsumingEnumerable()) 
    { 
        cts.Token.ThrowIfCancellationRequested();
        Console.WriteLine($"<<: {message}   (Consumer)");
    }
}


void RunProducer() 
{
    while (true)
    { 
        cts.Token.ThrowIfCancellationRequested();
        int i = random.Next(100);
        messages.Add(i);
        Console.WriteLine($">>: {i}   (Producer)");
        Thread.Sleep( random.Next(1000) );
    }
}