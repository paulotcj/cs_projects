//Note, without the lock, the final balance is random. Any tasks completing or trying to access the balance
// might cause a conflict with another task updating the balance.

var tasks = new List<Task>();
var ba1 = new BankAccount(0);
var ba2 = new BankAccount(0);

var mutex = new Mutex();
var mutex2 = new Mutex();

for (int i = 0; i < 10; ++i)
{
    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; ++j)
        {
            bool haveLock = mutex.WaitOne();
            try
            {
                ba1.Deposit(1); // deposit 10000 overall
            }
            finally
            {
                if (haveLock) mutex.ReleaseMutex();
            }
        }
    }));
    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; ++j)
        {
            bool haveLock = mutex2.WaitOne();
            try
            {
                ba2.Deposit(1); // deposit 10000
            }
            finally
            {
                if (haveLock) mutex2.ReleaseMutex();
            }
        }
    }));

    // transfer needs to lock both accounts
    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bool haveLock = Mutex.WaitAll(new[] { mutex, mutex2 });
            try
            {
                ba1.Transfer(ba2, 1); // transfer 10k from ba to ba2
            }
            finally
            {
                if (haveLock)
                {
                    mutex.ReleaseMutex();
                    mutex2.ReleaseMutex();
                }
            }
        }
    }));
}

Task.WaitAll(tasks.ToArray());

Console.WriteLine($"Final balance is: ba1={ba1.Balance}, ba2={ba2.Balance}.");




//--------------------------
public class BankAccount
{

    public BankAccount(int balance)
    {
        Balance = balance;
    }

    public int Balance { get; private set; }

    public void Deposit(int amount)
    {
        Balance += amount;
    }

    public void Withdraw(int amount)
    {
        Balance -= amount;

    }

    public void Transfer(BankAccount account, int amount) {
        Balance -= amount;
        account.Balance += amount;
    }
}






