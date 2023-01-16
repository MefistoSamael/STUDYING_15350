using System;
using laba2.Collections;
using laba2.Entities;

namespace laba2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyCustomCollection<string> list = new();

            Provider InfoSystem = new();

            Journal journal = new();
            InfoSystem.ChangeList += journal.Change;
            InfoSystem.UseTraffic += (string name) => Console.WriteLine($"Traffic was used by {name}.");

            InfoSystem.AddTariff("Bezlimitishe", 0.1);
            InfoSystem.AddTariff("SuperBezlimitishe", 0.2);
            InfoSystem.AddTariff("SIMPLE", 0.08);

            InfoSystem.AddClient("Alice", InfoSystem.tariffs[0], 125);
            InfoSystem.AddClient("Mark", InfoSystem.tariffs[0], 200);
            InfoSystem.AddClient("Jane", InfoSystem.tariffs[1], 90);
            InfoSystem.AddClient("Barbara", InfoSystem.tariffs[2], 333);

            InfoSystem.GetClients();
            InfoSystem.GetTariffs();

            try 
            { Console.WriteLine(InfoSystem.tariffs[222].Price); }
            catch (Exception ex)
            { Console.WriteLine($"\nОшибка: {ex.Message}"); }

            try
            { InfoSystem.tariffs.Remove(new("NONE", 0)); }
            catch (MyException ex)
            { 
                Console.WriteLine($"\nОшибка: {ex.Message}");
                Console.WriteLine($"Некорректное значение: {ex.Value}");
            }

            journal.Out();

            Console.WriteLine("Used traffic:");
            InfoSystem.WasUsed();
        }
    }
}
