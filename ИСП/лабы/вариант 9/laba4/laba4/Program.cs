using System;
using System.Collections.Generic;
using laba4.Entities;
using laba4.Collections;
using System.Linq;

namespace laba4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"D:\УНИК\прога\исп\laba4\laba41.dat";
            string path2 = @"D:\УНИК\прога\исп\laba4\laba42.dat";

            List<Competitor> competitors = new();
            competitors.Add(new("Katya", 14, true));
            competitors.Add(new("Philip", 15, false));
            competitors.Add(new("Artyom", 13, false));
            competitors.Add(new("Dasha", 17, false));
            competitors.Add(new("Nastya", 18, true));
            competitors.Add(new("Boris", 11, false));

            FileService fileService = new();
            fileService.SaveData(competitors, path1);

            fileService.RenameFile(path1, path2);

            var new_competitors = fileService.ReadFile(path2);

            Console.WriteLine("***После прочтения с файла:");
            foreach (var element in new_competitors)
                Console.WriteLine(element.Name + '\t' + element.Age + '\t' + element.Winner);
            Console.WriteLine(' ');

            var sorted_list1 = new_competitors.OrderBy(element => element, new MyCustomComparer());
            Console.WriteLine("***После первой сортировки:");
            foreach (var element in sorted_list1)
                Console.WriteLine(element.Name + '\t' + element.Age + '\t' + element.Winner);
            Console.WriteLine(' ');

            var sorted_list2 = new_competitors.OrderBy(element => element.Age);
            Console.WriteLine("***После второй сортировки:");
            foreach (var element in sorted_list2)
                Console.WriteLine(element.Name + '\t' + element.Age + '\t' + element.Winner);
            Console.WriteLine(' ');
        }
    }
}
