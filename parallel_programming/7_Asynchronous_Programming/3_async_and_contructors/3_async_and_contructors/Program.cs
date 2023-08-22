var a = new TestClass_1();
//var b = new TestClass_2(); //async in the constructor - impossible

Console.WriteLine($"------------------------------------");
var c = await new TestClass_3().InitAsync(); // you have to remember to do this and this is not ideal

Console.WriteLine($"------------------------------------");
//d is of type Task<TestClass_4>
var d = TestClass_4.CreateAsync();

Console.WriteLine($"------------------------------------");
//e is of type TestClass_4
var e = await TestClass_4.CreateAsync();




Console.WriteLine($"------------------------------------");
Console.WriteLine("Done");




public class TestClass_1 {
    public TestClass_1()
    {
        Console.WriteLine($"This is ok");
    }
}

/*
public class TestClass_2
{
    public TestClass_2()
    {
        await Task.Delay(1000); //This is impossible
        Console.WriteLine($"This is ok");
    }
}
*/

public class TestClass_3
{
    public TestClass_3()
    {
        Console.WriteLine($"TestClass_3 - This is ok");
    }

    public async Task<TestClass_3> InitAsync() 
    { 
        await Task.Delay(1000);
        Console.WriteLine($"TestClass_3.InitAsync() - This is ok");
        return this;
    }
}


public class TestClass_4
{
    private TestClass_4()
    {
        Console.WriteLine($"TestClass_4 - This is ok");
    }

    private async Task<TestClass_4> InitAsync()
    {
        await Task.Delay(1000);
        Console.WriteLine($"TestClass_4.InitAsync() - This is ok");
        return this;
    }

    public static Task<TestClass_4> CreateAsync()
    { 
        var result = new TestClass_4();
        return result.InitAsync();
    }
}
