using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laba2.Collections;

namespace laba2.Entities
{
    class Journal
    {
        List<string> changes;
        public Journal() => changes = new();

        public void AddChange(string info) => changes.Add(info);
        public void Change(string info) => changes.Add($"A new {info} was added!!!");
        public void Out()
        {
            Console.WriteLine("\nJournal:");
            foreach (var i in changes)
                Console.WriteLine(i);
            Console.WriteLine("-------------------");
        }
    }
}
