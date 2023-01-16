using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba3.Entities
{
    public class Client
    {
        public List<ValueTuple<Tariff, int>> tariffs = new ();
        public Client(string Name, Tariff tariff, int Min)
        {
            this.Name = Name;
            tariffs.Add(new(tariff, Min));
        }
        public void AddTariff(Tariff tariff, int Min) => tariffs.Add(new(tariff, Min));
        public string Name { get; set; }      
        private int Min { get; set; }

        public void ChangeTime(string tariffName, int Min)
        {
            for (int i = 0; i < tariffs.Count; i++)
                if (tariffs[i].Item1.Name == tariffName)
                    tariffs[i] = new(tariffs[i].Item1, Min);           
        }
        public double SpentMoney()
        {
            double result = 0;
            foreach (var element in tariffs)
                result += element.Item1.Price * element.Item2;

            return result;
        }
    }
}
