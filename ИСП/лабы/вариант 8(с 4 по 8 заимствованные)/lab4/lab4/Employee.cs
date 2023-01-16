using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name == null ? "NONAME" : name;
        }

        public override string ToString()
        {
            return $"ID сотрудника: {Id}\nИмя сотрудника: {Name}";
        }
    }
}
