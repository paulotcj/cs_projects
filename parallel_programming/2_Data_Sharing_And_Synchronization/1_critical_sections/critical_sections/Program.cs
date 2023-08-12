//Note, without the padlock, the final balance is random. Any tasks completing or trying to access the balance
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
    public object padlock = new object(); //organize and prevents illegal access to the critical region
    public int Balance { get; private set; }

    public void Deposit(int amount)
    {
        lock (padlock) //organize and prevents illegal access to the critical region
        {
            Balance += amount;
        }
    }

    public void Withdraw(int amount)
    {
        lock (padlock) //organize and prevents illegal access to the critical region
        {
            Balance -= amount;
        }
    }
}






