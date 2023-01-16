using System;
using laba3.Entities;

namespace laba3
{
    class Program
    {
        static void Main(string[] args)
        {
            Provider InfoSystem = new();
            InfoSystem.ChangeList += InfoSystem.Change;
            InfoSystem.UseTraffic += InfoSystem.UsingNow;

            InfoSystem.AddTariff("Безлимитище", 0.05);
            InfoSystem.AddTariff("ULTRA", 0.0245);
            InfoSystem.AddTariff("Супер", 0.075);

            InfoSystem.AddClient("Иван Петров", "Безлимитище", 5000);
            InfoSystem.NewClientTariff("Иван Петров", "ULTRA", 780);
            InfoSystem.NewClientTariff("Иван Петров", "Супер", 85);
            InfoSystem.AddClient("Пётр Валеевич", "Безлимитище", 7800);
            InfoSystem.AddClient("Дарья Демчук", "ULTRA", 2500);
            InfoSystem.AddClient("Александра Ткачук", "ULTRA", 5000);
            InfoSystem.NewClientTariff("Александра Ткачук", "Безлимитище", 6880);
            InfoSystem.AddClient("Константин Ковальчук", "Супер", 3110);           
            InfoSystem.AddClient("Мария Медведева", "Супер", 110);
            InfoSystem.NewClientTariff("Мария Медведева", "ULTRA", 9080);

            InfoSystem.GetTariffs();
            InfoSystem.GetClients();
            InfoSystem.OutJournal();

            Console.WriteLine("Общая стоимость всего трафика оператора = " + InfoSystem.AllPrice());
            Console.WriteLine("Лучший клиент = " + InfoSystem.GetBestClient());
            Console.WriteLine("Количество клиентов, заплативших больше 400 = " + InfoSystem.CountSpentNMoney(400) + '\n');

            InfoSystem.TariffPopularity();

            InfoSystem.WasUsed();
        }
    }
}