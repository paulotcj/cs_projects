//Note, without the lock, the final balance is random. Any tasks completing or trying to access the balance
// might cause a conflict with another task updating the balance.

var tasks = new List<Task>();
var ba = new BankAccount();

var mutex = new Mutex();

for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Factory.StartNew(() => {
        for (int j = 0; j < 1_000; j++) { 
            //----
            bool haveLock = mutex.WaitOne(1000);
            try { 
                ba.Deposit(100); 
            }
            finally { 
                if (haveLock) { mutex.ReleaseMutex(); } 
            }
            //----
        }
    }));

    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1_000; j++) {
            //----
            bool haveLock = mutex.WaitOne(1000);
            try
            {
                ba.Withdraw(100);
            }
            finally
            {
                if (haveLock) { mutex.ReleaseMutex(); }
            }
            //----            

        }
    }));
}

Task.WaitAll(tasks.ToArray());
Console.WriteLine($"Final balance: {ba.Balance}");


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






