using Entities;
using System.Reflection;

namespace Lab6;


class Program
{
    static void Main()
    {
        var employees = new List<Employee>()
        {
            new Employee(10, "Vasya"),
            new Employee(46, "Petya"),
            new Employee(91, "Katya"),
            new Employee(32, "Vova"),
            new Employee(90, "Sasha"),
            new Employee(18, "Vera"),
        };

        Assembly asm = Assembly.LoadFrom(@"C:\Шарпы каникулы\153501_Abramovich_Lab6\FileServices\bin\Debug\net6.0\FileServices.dll");

        Type? type = asm.GetType("FileServices.EmployeeFileService");

        if (type == null)
            throw new InvalidOperationException("Type not found.");

        //var constructor = type.GetConstructor(new Type[0]);
        //var obj = constructor.Invoke(null);

        var obj = Activator.CreateInstance(type,"File.txt");

        var saveData = type.GetMethod("SaveData");
        var readFile = type.GetMethod("ReadFile");

        if (saveData == null || readFile == null)
            throw new InvalidOperationException("Method not found");

        saveData.Invoke(obj, new object[] { employees });
        var col = readFile.Invoke(obj, null);

        if (col is IEnumerable<Employee> newEmployees)
        {
            foreach (var employee in newEmployees)
                Console.WriteLine(employee);
        }
        else
            throw new InvalidOperationException("Not correct file");
    }

}
