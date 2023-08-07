using System.Threading.Tasks;
static void Write(char c)
{
    int i = 1000;
    while (i-- > 0)
    {
        Console.Write(c);
    }
}

//-------------------------------

Task.Factory.StartNew(() => Write('.'));

var t = new Task(() => Write('#'));
t.Start();

Console.WriteLine("Hello, World!");
Console.ReadKey();




