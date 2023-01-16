using System;
using laba1.Collections;
using laba1.Entities;

namespace laba1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyCustomCollection<string> list = new();

            Provider InfoSystem = new();

            InfoSystem.AddTariff("Bezlimitishe", 0.1);
            InfoSystem.AddTariff("SuperBezlimitishe", 0.2);
            InfoSystem.AddTariff("SIMPLE", 0.08);

            InfoSystem.AddClient("Alice", InfoSystem.tariffs[0], 125);
            InfoSystem.AddClient("Mark", InfoSystem.tariffs[0], 200);
            InfoSystem.AddClient("Jane", InfoSystem.tariffs[1], 90);
            InfoSystem.AddClient("Barbara", InfoSystem.tariffs[2], 333);

            InfoSystem.GetClients();
            InfoSystem.GetTariffs();

            Console.WriteLine(InfoSystem.AllPrice());
            Console.WriteLine(InfoSystem.GetBestClient().Name);
        }
    }
}
