using System;
using System.Collections.Generic;
using System.Reflection;

namespace laba6
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\УНИК\прога\исп\laba6\laba6.json";

            Assembly asm = Assembly.LoadFrom(@"D:\УНИК\прога\исп\laba6\ClassLibrary\bin\Debug\net5.0\ClassLibrary.dll");
            Console.WriteLine(asm.FullName);

            var fileService = asm.GetType("ClassLibrary.FileService");
            if (fileService is null)
                return;
            MethodInfo save = fileService.GetMethod("SaveData");
            MethodInfo read = fileService.GetMethod("ReadFile");

            List<Employee> employees = new();
            employees.Add(new("Alexandr", 32, true));
            employees.Add(new("Roman", 56, false));
            employees.Add(new("Konstantin", 19, false));
            employees.Add(new("Alexandra", 21, false));
            employees.Add(new("Marina", 30, true));
            employees.Add(new("Galina", 49, true));

            object obj = Activator.CreateInstance(fileService);
            save.Invoke(obj, new object[] { employees, path });
            var result = read.Invoke(obj, new object[] { path });
            if (result is IEnumerable<Employee> new_employees)
                foreach (var element in new_employees)
                    Console.WriteLine(element);
        }
    }
}
