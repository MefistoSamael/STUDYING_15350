using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _153501_Bychko_Lab3.Collections
{
    public class MyCustomCollection<T> : _153501_Bychko_Lab3.Interfaces.ICustomCollection<T>, IEnumerable<T>
    {
        public class Node
        {
            public T Value { get; internal set; }

            public Node? Next { get; internal set; }

            public Node(T value)
            {
                Value = value;

                Next = null;
            }
        };

        public MyCustomCollection ()
        {
            First = null;
            Last = null;
            current = null;
        }

        public Node? First;

        public Node? Last;

        public Node? current;

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                    throw new ArgumentOutOfRangeException("Index is out of range");

                int i = 0;

                for (Node? node = First; node != null; node = node.Next, i++)
                    if (i == index)
                        return node.Value;

                throw new ArgumentOutOfRangeException("Index is out of range");
            }
            set
            {
                if (index < 0 || index > Count - 1)
                    throw new ArgumentOutOfRangeException("Index is out of range");

                int i = 0;

                for (Node? node = First; node != null; node = node.Next, i++)
                    if (i == index)
                        node.Value = value;

                throw new ArgumentOutOfRangeException("Index is out of range");
            }
        }

        public void Add(T item)
        {
            if (Count == 0)
            {
                First = Last = new Node(item);
            }
            else
            {
                Last!.Next = new Node(item);
                Last = Last.Next;
            }

                            Reset();
            Count++;
        }

        public T Current()
        {
            if (current != null) return current.Value;
            else throw new Exception("Current is null");
        }

        public void Next()
        {
            if (current != null) current = current.Next;
            else return;
        }

        public void Remove(T item)
        {
            if (item == null) throw new ArgumentNullException("item is null");
            if (Count == 0) throw new Exception("Collection is empty");

            //проверка current эллемента
            if (current != null && current!.Value!.Equals(item))
                current = null;

            //случай для головы
            if (item.Equals(First!.Value))
            {
                First = First.Next;
                Count--;
            }
            //случай для всего остального
            else
            {
                Node? nodeBeforeDelete = First;
                while (nodeBeforeDelete != null)
                {
                    if (nodeBeforeDelete.Next != null && nodeBeforeDelete.Next.Value!.Equals(item))
                        break;
                    nodeBeforeDelete = nodeBeforeDelete.Next;
                }
                if (nodeBeforeDelete == null)
                    throw new Exception($"Value {item} wasn't found");
                Node? nodeAfterDelete = nodeBeforeDelete!.Next!.Next;
                nodeBeforeDelete.Next = nodeAfterDelete;
                Count--;
            }

        }


        public T RemoveCurrent()
        {
            T value = current!.Value;
            Remove(current!.Value);
            return value;
        }

        public void Reset()
        {
            current = First;
        }

        public Node? Find(T value)
        {

            for (Node? node = First; node != null; node = node.Next)
                if (node.Value!.Equals(value))
                    return node;

            return null;
        }

        public Node? Find(Predicate<T> match)
        {

            for (Node? node = First; node != null; node = node.Next)
                if (match(node.Value))
                    return node;

            return null;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for(Node? node = First; node != null; node = node.Next)
            {
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
