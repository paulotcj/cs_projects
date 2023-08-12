using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;


// In this example we explore lock recursion. This style is prone to errors as it's difficult to control who took a lock
//   and when it occurred.

namespace DataSharingAndSynchronization
{
    class SpinLocking
    {
        class BankAccount
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


        static void Main(string[] args)
        {
            LockRecursion(5);

            Console.ReadKey();
            Console.WriteLine("All done here.");
        }

        static SpinLock sl = new SpinLock(true);

        private static void LockRecursion(int x)
        {
            // lock recursion TRIES to take the same lock multiple times, which will cause an exception
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e) //!!!!!!!!!!!!!!!! CHECK THIS !!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                Console.WriteLine("Exception: " + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}.");
                    LockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }
    }
}