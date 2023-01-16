using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace laba3.Entities
{
    class Provider
    {
        private List<Client> clients;
        public Dictionary<string, Tariff> tariffs;

        public Provider()
        {
            this.clients = new();
            this.tariffs = new();
        }

        public delegate void EventHandler(string info);
        public event EventHandler ChangeList;
        public event EventHandler UseTraffic;

        public List<string> journal = new();
        public void Change(string name) => journal.Add($"A new {name} was added!!!");
        public void UsingNow(string name) => Console.WriteLine($"Traffic was used by {name}.");

        public void AddClient(string Name, string tariffName, int Min)
        {
            if (tariffs.ContainsKey(tariffName))
                clients.Add(new(Name, tariffs[tariffName], Min));
            else
                clients.Add(new(Name, new("NONE", 0), 0));

            ChangeList?.Invoke("client " + Name);
        }
        public void NewClientTariff(string clientName, string tariffName, int Min)
        {
            foreach (var element in clients)
                if (element.Name == clientName)
                    element.AddTariff(tariffs[tariffName], Min);
        }
        public void AddTariff(string Name, double Price)
        {
            tariffs.Add(Name, new(Name, Price));

            ChangeList?.Invoke("tariff " + Name);
        }
        public void GetClients()
        {
            var elements = clients.OrderBy(client => client.Name); // методы расширения linq

            foreach (var element in elements) 
                Console.WriteLine(element.Name);
            Console.WriteLine("-------------------");
        }
        public void GetTariffs()
        {
            var elements = from tariff in tariffs   // операторы запросов linq
                           orderby tariff.Value.Price
                           select tariff;

            foreach (var element in elements)
                Console.WriteLine(element.Value.Name + '\t' + element.Value.Price);
            Console.WriteLine("-------------------");
        }
        public double AllPrice()
        {
            double result = 0;
            foreach (var element in clients)
                result += element.SpentMoney();

            return result;
        }
        public string GetBestClient()
        {
            var elements = clients.OrderByDescending(client => client.SpentMoney());
            return elements.First().Name;
        }
        public int CountSpentNMoney(double N) => 
            clients.Aggregate(0, (result, client) => client.SpentMoney() > N ? result + 1 : result);
        public void TariffPopularity()
        {
            List<Tuple<string, string>> list = new();
            foreach (var element in clients)
                for (int i = 0; i < element.tariffs.Count; i++)
                    list.Add(new(element.Name, element.tariffs[i].Item1.Name));

            var users = from user in list
                        group user by user.Item2 into Group
                        select new { tariffName = Group.Key, Count = Group.Count() };               

            foreach (var element in users)
                Console.WriteLine(element.tariffName + '\t' + element.Count);
            Console.WriteLine("-------------------");
        }
        public void WasUsed()
        {
            while (true)
                foreach (var i in clients)
                {
                    UseTraffic?.Invoke(i.Name);
                    Thread.Sleep(2000);
                }
        }

        public void OutJournal()
        {
            Console.WriteLine("\nJournal:");
            foreach (var i in journal)
                Console.WriteLine(i);
            Console.WriteLine("-------------------");
        }
    }
}
