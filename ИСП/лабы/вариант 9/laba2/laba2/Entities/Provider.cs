using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using laba2.Collections;

namespace laba2.Entities
{
    class Provider
    {
        private MyCustomCollection<Client> clients;
        public MyCustomCollection<Tariff> tariffs;

        public Provider()
        {
            this.clients = new();
            this.tariffs = new();
        }

        public delegate void Event(string info);
        public event Event ChangeList;
        public event Event UseTraffic;

        public void AddClient(string Name, Tariff tariff, int Min)
        {
            clients.Add(new(Name, tariff, Min));

            ChangeList?.Invoke("client " + Name);
        }

        public void AddTariff(string Name, double Price)
        {
            tariffs.Add(new(Name, Price));

            ChangeList?.Invoke("tariff " + Name);
        }

        public double AllPrice()
        {
            double price = 0;

            for (int i = 0; i < clients.Count; i++)
                price += clients[i].tariff.Price * clients[i].Min;

            return price;
        }
        public Client GetBestClient()
        {
            Client best;
            if (clients.Count != 0)
                best = this.clients[0];
            else
                best = new("NONE", new("NONE", 0), 0);

            for (int i = 0; i < clients.Count - 1; i++)
                if (this.clients[i + 1].SpentMoney() > best.SpentMoney())
                    best = this.clients[i + 1];

            return best;
        }

        public void GetClients()
        {
            foreach (var element in clients)
                Console.WriteLine(element.Name + ' ' + element.tariff.Name + ' ' + element.Min);
            Console.WriteLine("-------------------");
        }

        public void GetTariffs()
        {
            foreach (var element in tariffs)
                Console.WriteLine(element.Name + ' ' + element.Price);
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
    }
}
