using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2.Collections
{
    class MyCollectionEnumerator<A> : IEnumerator<A>
    {
        public MyCustomCollection<A> collection;
        int position = -1;
        public MyCollectionEnumerator(MyCustomCollection<A> collection) => this.collection = collection;

        public A Current
        {
            get
            {
                if (position == -1 || position >= collection.Count)
                    throw new ArgumentException();
                return collection[position];
            }
        }
        object IEnumerator.Current => this.Current;
        public void Dispose() { }
        public bool MoveNext()
        {
            if (position < collection.Count - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }
        public void Reset() => position = -1;
    }
}
