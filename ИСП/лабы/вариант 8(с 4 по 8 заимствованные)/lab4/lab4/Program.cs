using lab4;
using System.IO;
{
    List < Employee> employees = new List<Employee>()
    {
        new(12, "Anatoliy"),
        new(25, "Genadiy"),
        new(66, "John"),
        new(17, "Leon"),
        new(99, "Filip"),
    };

    FileService fs = new();

    fs.SaveData(employees, "empl.dat");

    if (File.Exists("Employees.dat"))
        File.Delete("Employees.dat");

    File.Move("empl.dat", "Employees.dat");

    var newEmployees = fs.ReadFile("Employees.dat");

    foreach (Employee employee in newEmployees)
        Console.WriteLine(employee);

    Console.WriteLine();

    var newEmployees1 = newEmployees.OrderBy(t => t, new MyCustomComparer());
    foreach (Employee employee in newEmployees1)
        Console.WriteLine(employee);

    Console.WriteLine();
    var newEmployees2 = newEmployees.OrderBy(t => t.Id);
    foreach (Employee employee in newEmployees2)
        Console.WriteLine(employee);
}