using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

var capitals = new ConcurrentDictionary<string, string>();
void AddParis() {
    bool success = capitals.TryAdd("France", "Paris");
    var who = Task.CurrentId.HasValue ? ($"Task {Task.CurrentId}") : "Main Thread";
    Console.WriteLine($"{who} - Sucess:{success}");

}

var t1 = Task.Factory.StartNew(AddParis);
var t2 = Task.Factory.StartNew(AddParis);


AddParis();

capitals["Russia"] = "Leningrad";
capitals["Russia"] = "Moscow"; //This can cause overwriting problems between tasks
capitals.TryAdd("Russia", "qwert");


Console.WriteLine($"Capital of Russia: {capitals["Russia"]}");
//------------

//Lets restart the example
capitals["Russia"] = "Leningrad";
capitals.AddOrUpdate("Russia", "Moscow", (k, v_old) => { return v_old + " --> Moscow"; });

Console.WriteLine($"Capital of Russia: {capitals["Russia"]}");

//------------
capitals["Sweden"] = "Uppsala"; //This will now be overwritten
var cap_sweden = capitals.GetOrAdd("Sweden", "Stockholm");
Console.WriteLine($"Capital of Sweden: {cap_sweden}");


//------------
var cap_uk = capitals.GetOrAdd("Uk", "London"); //This is not defined in the dictionary, therefore we will use "London"
Console.WriteLine($"Capital of UK: {cap_uk}");

//------------

string to_remove = "Russia";
string removed;
bool did_remove = capitals.TryRemove(to_remove, out removed);
Console.WriteLine($"Tried to remove from: {to_remove}, output: {removed}, success flag: {did_remove}");

//------------

to_remove = "Spain";
removed = null;
did_remove = capitals.TryRemove(to_remove, out removed);
Console.WriteLine($"Tried to remove from: {to_remove}, output: {removed}, success flag: {did_remove}");


//------------
int count = capitals.Count(); //this is expensive for this type of collection
Console.WriteLine($"Capitals count = {count}");

Console.WriteLine("Done");
