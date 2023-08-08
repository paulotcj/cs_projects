using System.Threading.Tasks;
static void Write(char c)
{
    int i = 1000;
    while (i-- > 0)
    {
        Console.Write(c);
    }
}

static void Write2(object o)
{
    int i = 1000;
    while (i-- > 0)
    {
        Console.Write(o);
    }
}

static int TextLength(object o)
{
    Console.WriteLine($"\nTask with id {Task.CurrentId} processing object '{o}...'");
    return o.ToString().Length;
}


//-------------------------------

Task.Factory.StartNew(() => Write('.'));
var t = new Task(() => Write('#'));
t.Start();
Console.ReadKey();

//-----------------------
Task t2 = new Task(Write2, "HI");
t2.Start();
Task t3 = new Task(Write2, 123);
t3.Start();
Console.ReadKey();

//-----------------------
string text1 = "text1", text2 = "this";

var t4 = new Task<int>(TextLength, text1);
t4.Start();

Task<int> t5 = Task.Factory.StartNew(TextLength, text2);

Console.WriteLine($"Length of '{text1}' is {t4.Result}");
Console.WriteLine($"Length of '{text2}' is {t5.Result}");




Console.WriteLine("Hello, World!");
Console.ReadKey();




