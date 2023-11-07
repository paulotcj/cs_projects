Console.WriteLine("Hello, World!");

int[] array_nums = new int[]{ 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20 };

int start_idx = 2;
int len = 5;

int[] slice = new int[len];

Array.Copy(sourceArray: array_nums, sourceIndex: start_idx, destinationArray: slice, destinationIndex: 0, length: len);

Console.WriteLine($"Slice: {string.Join(", ", slice)}");
Console.WriteLine("--------------------");


array_nums = new int[] { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20 };
 start_idx = 2;
 len = 6;

 slice = array_nums.Skip(start_idx).Take(len).ToArray();

Console.WriteLine($"Slice: {string.Join(", ", slice)}");
Console.WriteLine("--------------------");

slice = array_nums[2..];
Console.WriteLine($"Slice: {string.Join(", ", slice)}");
Console.WriteLine("--------------------");

slice = array_nums[..2];
Console.WriteLine($"Slice: {string.Join(", ", slice)}");
Console.WriteLine("--------------------");

slice = array_nums[2..9]; // 2+7 = 9
Console.WriteLine($"Slice: {string.Join(", ", slice)}");
Console.WriteLine("--------------------");

slice = array_nums[..]; 
Console.WriteLine($"Slice: {string.Join(", ", slice)}");
Console.WriteLine("--------------------");


slice = array_nums[2..9];
array_nums[2] = 100;
array_nums[3] = 101;
array_nums[4] = 102;
array_nums[5] = 103;
Console.WriteLine($"Slice: {string.Join(", ", slice)}");
Console.WriteLine($"array_nums: {string.Join(", ", array_nums)}");




