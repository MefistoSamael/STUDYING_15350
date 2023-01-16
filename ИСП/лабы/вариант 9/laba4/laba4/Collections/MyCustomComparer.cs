using System.Collections.Generic;
using laba4.Entities;

namespace laba4.Collections
{
    class MyCustomComparer : IComparer<Competitor>
    {
        public int Compare(Competitor x, Competitor y)
        {
            if (x == null && y == null) 
                return 0;
            else if (x == null) 
                return -1;
            else if (y == null) 
                return 1;
            else 
                return x.Name.CompareTo(y.Name);
        }
    }
}
