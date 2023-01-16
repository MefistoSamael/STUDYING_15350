using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2.Entities
{
    public class Client
    {
        public Client(string Name, Tariff tariff, int Min)
        {
            this.Name = Name;
            this.tariff = tariff;
            this.Min = Min;
        }
        public void AddTariff(Tariff tariff) => this.tariff = tariff;
        public void ChangeTime(int Min) => this.Min = Min;
        public string Name { get; set; }
        public Tariff tariff { get; set; }
        public int Min { get; set; }
        public double SpentMoney() => tariff.Price * Min;
    }
}
