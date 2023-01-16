using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laba1.Collections;

namespace laba1.Entities
{
    class Provider
    {
        public MyCustomCollection<Client> clients;
        public MyCustomCollection<Tariff> tariffs;

        public Provider()
        {
            this.clients = new();
            this.tariffs = new();
        }

        public void AddClient(string Name, Tariff tariff, int Min)
        {
            clients.Add(new(Name, tariff, Min));
        }

        public void AddTariff(string Name, double Price)
        {
            tariffs.Add(new(Name, Price));
        }

        public double AllPrice()
        {
            double price = 0;

            for (int i = 0; i < clients.Count; i++)
            {
                price += clients[i].tariff.Price*clients[i].Min;
            }

            return price;
        }
        public Client GetBestClient()
        {
            Client best;
            if (clients.Count != 0)
            {
                best = this.clients[0];
            }
            else
            {
                best = new("NONE", new("NONE", 0), 0);
            }

            for (int i = 0; i < clients.Count - 1; i++)
            {
                if(this.clients[i+1].SpentMoney()>best.SpentMoney())
                {
                    best = this.clients[i + 1];
                }
            }

            return best;
        }

        public void GetClients()
        {
            for (int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine(clients[i].Name + '\t' + clients[i].tariff.Name + '\t' + clients[i].Min);
            }
            Console.WriteLine("-------------------");
        }

        public void GetTariffs()
        {
            for (int i = 0; i < tariffs.Count; i++) 
            {
                Console.WriteLine(tariffs[i].Name + '\t' + tariffs[i].Price);
            }
            Console.WriteLine("-------------------");
        }
    }
}
