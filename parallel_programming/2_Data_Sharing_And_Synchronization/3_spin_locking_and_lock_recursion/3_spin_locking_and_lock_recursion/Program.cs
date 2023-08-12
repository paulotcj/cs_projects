//Note, without the lock, the final balance is random. Any tasks completing or trying to access the balance
// might cause a conflict with another task updating the balance.

var tasks = new List<Task>();
var ba = new BankAccount();

SpinLock sl = new SpinLock();

for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Factory.StartNew(() => {
        for (int j = 0; j < 1_000; j++) {
            //----
            var lockTaken = false;
            try
            {
                // Alternatvely you can use for time-out configuration: sl.TryEnter(int_timeout_in_ms, bool_lock); 
                sl.Enter(ref lockTaken);
                ba.Deposit(100);
            }
            finally {
                if (lockTaken) { sl.Exit(); }
            }
            //----
        }
    }));

    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1_000; j++) {
            //----
            var lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
                ba.Withdraw(100);
            }
            finally
            {
                if (lockTaken) { sl.Exit(); }
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






