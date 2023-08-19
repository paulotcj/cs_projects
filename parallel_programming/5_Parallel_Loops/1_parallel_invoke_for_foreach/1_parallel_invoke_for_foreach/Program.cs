

Console.WriteLine($"These tasks will execute out of the predetermined order");
var a = new Action(() => { Console.WriteLine($"1st action - Task ID: {Task.CurrentId}"); });
var b = new Action(() => { Console.WriteLine($"2nd action - Task ID: {Task.CurrentId}"); });
var c = new Action(() => { Console.WriteLine($"3rd action - Task ID: {Task.CurrentId}"); });
var d = new Action(() => { Console.WriteLine($"4th action - Task ID: {Task.CurrentId}"); });

Parallel.Invoke(a,b,c,d);


Console.WriteLine($"------------------------");
Console.WriteLine($"This is a 'FOR Parallel' loop");
Parallel.For(fromInclusive: 1, toExclusive: 11, i => {
    Console.WriteLine($"Task ID: {Task.CurrentId} - i: {i}");
});


Console.WriteLine($"------------------------");
Console.WriteLine($"This is a 'FOREACH Parallel' loop");
string[] strs = { "1st", "2nd" , "3rd" ,"4th" , "5th" };
Parallel.ForEach(source: strs, element => {
    Console.WriteLine($"Element: {element} - Task ID: {Task.CurrentId}");
});



Console.WriteLine($"------------------------");
Console.WriteLine($"This is a 'FOREACH Parallel' loop with a custom function");
Parallel.ForEach(CustomRange(start: 1, end: 20, step: 1), element => {
    Console.WriteLine($"Element: {element} - Task ID: {Task.CurrentId}");
});




Console.WriteLine($"------------------------");
Console.WriteLine("All Done");


//------------------------------------
IEnumerable<int> CustomRange(int start, int end, int step) {
    for (int i = start; i < end; i += step ) { 
        yield return i;
    }
}
