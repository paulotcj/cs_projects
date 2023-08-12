//Note, without the lock, the final balance is random. Any tasks completing or trying to access the balance
// might cause a conflict with another task updating the balance.

var tasks = new List<Task>();
var ba = new BankAccount();

for (int i = 0; i < 10; i++) {
    tasks.Add(  Task.Factory.StartNew(() => { 
        for (int j = 0; j < 1_000; j++) { ba.Deposit(100); }
    })  );

    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1_000; j++) { ba.Withdraw(100); }
    }));
}

Task.WaitAll(tasks.ToArray());
Console.WriteLine($"Final balance: {ba.Balance}");
//Console.WriteLine("Done!");
//Console.ReadKey();

//--------------------------
public class BankAccount
{

    //-------
    // we had to rework this property
    private int balance;
    public int Balance {
        get { return balance; }
        private set { balance = value; }
    }
    //-------

    public void Deposit(int amount)
    {
        //this basically makes the operation atomic and no other task can enter this protected
        //  region while this operation is taking place
        // This method is known as lock-free programming
        Interlocked.Add(ref balance, amount);
        //Balance += amount;

        //There are other methods to be explored within the Interlocked. Methods such as Exchange, which
        // basically sets the value to a target variable.
    }

    public void Withdraw(int amount)
    {
        //this basically makes the operation atomic and no other task can enter this protected
        //  region while this operation is taking place
        Interlocked.Add(ref balance, -amount);
        //Balance -= amount;
    }

    public void Mem_Barrier_Test() {
        //MEMORY BARRIER
        // Due to how a processor executes instructions and fetches data from memory, a set of instructions defined such as:
        //  1, 2, 3   may execute as 3,1,2 .
        //  And if the execution order is important you might want to use
        Thread.MemoryBarrier();
        //It tells the CPU that in no instance the instructions that appear before the memory barrier instruction be 
        // executed in the block after the memory barrier
    }

}






