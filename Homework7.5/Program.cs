using System.Reflection;

public enum Role
{
    Manager,
    Programmer,
    Director
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
class AccessLevelAttribute : Attribute
{
    public Role _role { get; }
    public AccessLevelAttribute(Role Role)
    {
        _role = Role;
    }
}


public abstract class Employee
{
    public string Name { get; set; }
    protected Employee(string name) => Name = name;
}

[AccessLevel(Role.Manager)]
class Manager : Employee
{
    public Manager(string name) : base(name) { }
}

[AccessLevel(Role.Programmer)]
class Programmer : Employee
{
    public Programmer(string name) : base(name) { }
}

[AccessLevel(Role.Director)]
class Director : Employee
{
    public Director(string name) : base(name) { }
}

public class SecureSystem
{
    private readonly Role[] allowedRoles = { Role.Programmer, Role.Director };

    public void CheckAccess(Employee employee)
    {
        Type type = employee.GetType();
        var attr = type.GetCustomAttribute<AccessLevelAttribute>();

        if (attr != null)
        {
            if (Array.Exists(allowedRoles, r => r == attr._role))
            {
                Console.WriteLine($"{employee.Name} ({attr._role}): ДОСТУП ДОЗВОЛЕНО");
            }
            else
            {
                Console.WriteLine($"{employee.Name} ({attr._role}): ДОСТУП ЗАБОРОНЕНО");
            }
        }
        else
        {
            Console.WriteLine($"{employee.Name} ({type.Name}): Рівень доступу не визначено!");
        }
    }
}
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var employees = new Employee[]
        {
            new Manager("Олег"),
            new Programmer("Ірина"),
            new Director("Марія")
        };

        SecureSystem system = new SecureSystem();

        foreach (var emp in employees)
        {
            system.CheckAccess(emp);
        }
    }
}