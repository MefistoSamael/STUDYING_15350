using lab6;
using System.Reflection;

{
    //динамически загружаем библиотеку
    Assembly asm = Assembly.LoadFrom(@"E:\!учеба\ИСП\lab6\Debug\net6.0\FileServiceLib.dll");

    //получаем тип FileService,
    //`1 значит, что класс обощенный с 1 обобщенной переменной
    Type? t = asm.GetType("FileServiceLib.FileService`1");

    if (t is not null)
    {
        //передаем тип Employee в класс FileService
        t = t.MakeGenericType(typeof(Employee));
        //получаем необходимые методы
        MethodInfo? ReadFile = t.GetMethod("ReadFile");
        MethodInfo? SaveData = t.GetMethod("SaveData");

        List<Employee> employee = new List<Employee>()
        {
            new Employee(12, false, "Petr"),
            new Employee(255, true, "Vassily"),
            new Employee(28, false, "Gosha"),
            new Employee(63, false, "Inokentiy"),
            new Employee(672, false, "Ekaterina"),
        };
        //создаем объект FileService<Employee>
        var obj = Activator.CreateInstance(t);

        string path = "employee.json";
        SaveData?.Invoke(obj, new object[] { employee, path });

        List<Employee>? result = new List<Employee>(ReadFile!.Invoke(obj, new object[] { path }) as List<Employee>);

        if (result == null)
            return;
        foreach(Employee emp in result)
        {
            Console.WriteLine(emp);
        }
    }
}
