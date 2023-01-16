using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using laba5.Domain;
using Serializer;

namespace _153501_Bybko_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"D:\УНИК\прога\исп\laba5\laba5.xml";
            string path2 = @"D:\УНИК\прога\исп\laba5\laba5.json";
            string path3 = @"D:\УНИК\прога\исп\laba5\laba5.txt";

            List<Computer> computers = new();
            computers.Add(new("Monitor1"));
            computers.Add(new("Monitor2"));
            computers.Add(new("Monitor3"));
            computers.Add(new("Monitor4"));
            computers.Add(new("Monitor5"));
            computers.Add(new("Monitor6"));

            Serializer.Serializer serializer = new();

            serializer.SerializeXML(computers, path1);
            var elements1 = serializer.DeSerializeXML(path1);
            Console.WriteLine("***Сериализация и десериализация XML:");
            foreach (var element in elements1)
                Console.WriteLine(element.monitor.Name);
            Console.WriteLine(' ');

            serializer.SerializeJSON(computers, path2);
            var elements2 = serializer.DeSerializeJSON(path2);
            Console.WriteLine("***Сериализация и десериализация JSON:");
            foreach (var element in elements2)
                Console.WriteLine(element.monitor.Name);
            Console.WriteLine(' ');

            serializer.SerializeByLINQ(computers, path3);
            var elements3 = serializer.DeSerializeByLINQ(path3);
            Console.WriteLine("***Сериализация и десериализация LINQ-to-XML:");
            foreach (var element in elements3)
                Console.WriteLine(element.monitor.Name);
            Console.WriteLine(' ');

            while(true)
            {
                Console.WriteLine("***Какой файл хотите открыть?\n1. XML\n2. JSON\n3. LINQ-to-XML\n4. exit\n");
                int choice;
                while(true)
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 && choice <= 4) break;
                    Console.WriteLine("Неверные данные. Повторите ещё раз!!!");
                }

                switch (choice)
                {
                    case 1:
                        System.Diagnostics.Process.Start("notepad.exe", path1);
                        break;
                    case 2:
                        System.Diagnostics.Process.Start("notepad.exe", path2);
                        break;
                    case 3:
                        System.Diagnostics.Process.Start("notepad.exe", path3);
                        break;
                    case 4:
                        return;
                }
            }           
        }
    }
}
