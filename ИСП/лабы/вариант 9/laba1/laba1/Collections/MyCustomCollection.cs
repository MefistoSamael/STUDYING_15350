using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laba1.Interfaces;

namespace laba1.Collections
{
    internal class Node<A>
    {
        public Node()
        {
            this.Next = null;
            this.Prev = null;
        }
        public Node(A Data)
        {
            this.Data = Data;
            this.Next = null;
            this.Prev = null;
        }
        public A Data { get; set; }
        public Node<A> Next { get; set; }
        public Node<A> Prev { get; set; }
    }
    internal class MyCustomCollection<A> : ICustomCollection<A>
    {
        private Node<A> Cursor;
        private Node<A> Head;
        private Node<A> Tail;

        public MyCustomCollection()
        {
            this.Head = null;
            this.Tail = null;
            this.Cursor = null;
            this.Count = 0;
        }
        public A this[int index]
        {
            get
            {
                Node<A> temp = Head;
                for (int i = 0; i < index; i++)
                {
                    temp = temp.Next;
                }
                return temp.Data;
            }

            set
            {
                Node<A> temp = Head;
                for (int i = 0; i < index; i++)
                {
                    temp = temp.Next;
                }
                temp.Data = value;
            }
        }

        public void Reset()
        {
            Cursor = Head;
        }
        public void Next()
        {
            Cursor = Cursor.Next;
            if (Cursor == null) Cursor = Head;
        }
        public A Current()
        {
            return Cursor.Data;
        }
        public int Count { get; private set; }
        public void Add(A item)
        {
            Node<A> node = new(item);

            if (this.Count == 0)
            {
                this.Head = node;
                this.Tail = node;
            }
            else
            {
                this.Tail.Next = node;
                node.Prev = Tail;
                this.Tail = node;
            }
            Cursor = Tail;
            this.Count++;
        }
        public void Remove(A item)
        {
            if (Count == 0) return;

            Reset();
            for (int i = 0; i < Count; i++)
            {
                if (Cursor.Data.Equals(item)) break;
                if (Cursor == null) return;

                Next();
            }

            if (Cursor == Tail)
            {
                Tail = Cursor.Prev;
                Tail.Next = null;
            }
            else
            {
                Cursor.Prev.Next = Cursor.Next;
                Cursor.Next.Prev = Cursor.Prev;
            }
            Next();
            Count--;
        }
        public void RemoveCurrent()
        {
            if (Cursor == null || Count == 0) return;

            if (Cursor == Tail)
            {
                Tail = Cursor.Prev;
                Tail.Next = null;
            }
            else if (Cursor == Head)
            {
                Head = Cursor.Next;
                Head.Prev = null;
            }
            else
            {
                Cursor.Prev.Next = Cursor.Next;
                Cursor.Next.Prev = Cursor.Prev;
            }
            Next();
            Count--;
        }

        public void Out()
        {
            Reset();
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine(Cursor.Data);
                Next();
            }
            Console.WriteLine("-------------------");
        }
    }
}
