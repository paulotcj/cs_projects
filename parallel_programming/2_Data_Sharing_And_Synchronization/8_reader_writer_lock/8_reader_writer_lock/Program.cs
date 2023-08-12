//Note, without the lock, the final balance is random. Any tasks completing or trying to access the balance
// might cause a conflict with another task updating the balance.

var padlock = new ReaderWriterLockSlim();
Random random = new Random();

int x = 0;

var tasks = new List<Task>();
for (int i = 0; i < 10; i++) { 
    tasks.Add(Task.Factory.StartNew(() => {

        //Alternativerly you could use the following in case you might need to upgrade your lock
        // from read to write:
        //     padlock.EnterUpgradeableReadLock();
        // and then:
        //     padlock.EnterWriteLock();
        //     x = 123;
        //     padlock.ExitWriteLock();
        // and finally you should use:
        //     padlock.ExitUpgradeableReadLock();

        
        padlock.EnterReadLock();

        Console.WriteLine($"Entered read lock, x = {x}");
        Thread.Sleep( 5000 );

        padlock.ExitReadLock();

        Console.WriteLine($"Exited read lock, x = {x}");
    }));
}

//You need to wait for the reads to finish before trying to start a write operation
// or you might cause a deadlock
try {
    Task.WaitAll(tasks.ToArray());
}
catch ( AggregateException ae ) {
    ae.Handle(e => {
        Console.WriteLine(e);
        return true;
    });
}

//while (true) { 
for (int i = 0; i < 10; i++)
{
    //Console.ReadKey();
    padlock.EnterWriteLock();
    int newValue = random.Next(10);
    x = newValue;
    Console.WriteLine($"x = {x}");
    padlock.ExitWriteLock();
    Console.WriteLine($"Write lock released.");
}

//--------------------------
public class BankAccount
{

    public int Balance { get; private set; }

    public void Deposit(int amount)
    {
        Balance += amount;
    }

    public void Withdraw(int amount)
    {
        Balance -= amount;
    }
}






