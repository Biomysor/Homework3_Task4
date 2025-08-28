
class MyClass
{
    [Obsolete("Попередження!!!")]
    public void Method()
    {
        Console.WriteLine("Метод.");
    }

    [Obsolete("Використання заборонено!", true)]
    public void Method2()
    {
        Console.WriteLine("Метод2.");
    }
}
class Program
{
    private static void Main(string[] args)
    {
        MyClass instance = new MyClass();

        instance.Method();
        //instance.Method2();

    }
}