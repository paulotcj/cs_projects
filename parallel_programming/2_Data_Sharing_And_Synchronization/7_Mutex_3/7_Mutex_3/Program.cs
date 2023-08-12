//For this example,
// - Open 2 or more terminal windows.
// - On each terminal window change the directory to the path where the "Program.cs" is
// - Execute on each window: >dotnet run .\Program.cs
//
// On the first window you should see: "We can run the program just fine."
// On the other windows you will see: "Sorry, MyApp is already running."


GlobalMutex();

Console.WriteLine("All done here.");


static void GlobalMutex() 
{
    const string appName = "MyApp";
    Mutex mutex;

    try { 
        mutex = Mutex.OpenExisting(appName);
        Console.WriteLine($"Sorry, {appName} is already running.");
    }
    catch(WaitHandleCannotBeOpenedException e) {
        Console.WriteLine($"We can run the program just fine.");
        // first arg = whether to give current thread initial ownership
        mutex = new Mutex(false, appName);
    }

    Console.ReadKey();
}

//static void GlobalMutex()
//{
//    const string appName = "MyApp";
//    Mutex mutex;
//    try
//    {
//        mutex = Mutex.OpenExisting(appName);
//        Console.WriteLine($"Sorry, {appName} is already running.");
//        return;
//    }
//    catch (WaitHandleCannotBeOpenedException e)
//    {
//        Console.WriteLine("We can run the program just fine.");
//        // first arg = whether to give current thread initial ownership
//        mutex = new Mutex(false, appName);
//    }

//    Console.ReadKey();
//}