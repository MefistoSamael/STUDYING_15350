using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        bool isPolyglot { get; set; }

        public Employee()
        {
            Id = 0;
            isPolyglot = true;
            Name = "NONAME";
        }

        public Employee(int id, bool puska, string name)
        {
            Id = id;
            isPolyglot = puska;
            Name = name == null ? "NONAME" : name;
        }

        public override string ToString()
        {
            return "Работник: " + Name + "; Id: " + Id;
        }
    }
}
