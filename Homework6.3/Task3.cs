using System.Reflection;

class Task3
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Assembly assembly = null;

        try
        {
            //assembly = Assembly.Load("TempratureLibrary"); // Не працює (((
            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempratureLibrary.dll");
            assembly = Assembly.LoadFrom(dllPath);
            Console.WriteLine("Loaded TempratureLibrary assembly");

            Type type = assembly.GetType("TempratureLibrary.Class1");
            if (type == null)
            {
                Console.WriteLine("Клас TempratureLibrary.Class1 не знайдено!");
                return;
            }

            dynamic temperatureInstance = Activator.CreateInstance(type);

            Console.WriteLine("Введіть значення температури за цельсієм: ");

            double valueCelsius = Convert.ToDouble(Console.ReadLine());
            double result = temperatureInstance.CelsiusToFahrenheit(valueCelsius);

            Console.WriteLine($"\n{valueCelsius} градусів за Цельсієм буде: {result} по Фарангейту.");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}