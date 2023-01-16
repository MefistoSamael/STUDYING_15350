using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    internal class MyCustomComparer : IComparer<Employee>
    {
        public int Compare(Employee? first, Employee? second)
        {
            if (first == null && second == null)
                return 0;
            if (first == null)
                return 1;
            if(second == null)
                return -1;
            return first.Name.CompareTo(second.Name);
        }
    }
}
