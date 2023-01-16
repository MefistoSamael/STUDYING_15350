using System.Collections;


namespace Collections;


public class RemoveException : ArgumentException
{
    public RemoveException(string message) 
        : base(message) { }
}

public class CustomCollection<T> : IEnumerable<T>
{
    public class Node
    {
        public T Value { get; set; }

        public Node? Next { get; internal set; }
        public Node? Prev { get; internal set; }

        public CustomCollection<T>? Col { get; internal set; }


        public Node(T value)
        {
            Value = value;

            Col = null;
            Next = Prev = null;
        }

        internal void Separate()
        {
            Col = null;
            Next = Prev = null;
        }
    }


    public Node? First { get; private set; }
    public Node? Last { get; private set; }

    public T Current
    {
        get
        {
            Node? cur = _current;

            if (cur == null)
                throw new IndexOutOfRangeException("Invalid iterator");

            return cur.Value;
        }
    }
    private Node? _current { get; set; }

    public int Version { get; private set; }

    private int _count;
    public int Count
    {
        get => _count;
        private set
        {
            Version++;
            _count = value;
        }
    }

    public bool IsReadOnly { get; }


    public CustomCollection()
    {
        Last = First = _current = null;
        Version = Count = 0;
        IsReadOnly = false;
    }
    public CustomCollection(IEnumerable<T> col) : this()
    {
        if (col == null)
            throw new ArgumentNullException("Collection is null.");

        foreach (T item in col)
            Add(item);
    }


    public void Add(Node node)
    {
        if (node == null)
            throw new ArgumentNullException("Node is null.");

        if (node.Col != null)
            throw new InvalidOperationException("Node already belongs to list.");

        node.Col = this;

        if (IsEmpty())
        {
            Last = First = _current = node;
            Count = 1;
            return;
        }

        Last!.Next = node;
        node.Prev = Last;

        Last = node;
        Count++;
    }
    public void Add(T value)
    {
        Add(new Node(value));
    }


    public void RemoveFirst()
    {
        if (IsEmpty())
            throw new InvalidOperationException("List is empty.");

        if (Count == 1)
        {
            Clear();
            return;
        }

        if (_current == First)
            _current = null;

        Count--;

        First = First!.Next;
        First!.Prev!.Separate();
        First!.Prev = null;
    }

    public void RemoveLast()
    {
        if (IsEmpty())
            throw new InvalidOperationException("List is empty.");

        if (Count == 1)
        {
            Clear();
            return;
        }

        if (_current == Last)
            _current = null;

        Count--;

        Last = Last!.Prev;
        Last!.Next!.Separate();
        Last!.Next = null;
    }

    public void Remove(Node node)
    {
        if (node == null)
            throw new NullReferenceException("Node is null.");

        if (node.Col != this)
            throw new InvalidOperationException("Node don't belongs to current list.");

        if (node == First)
        {
            RemoveFirst();
            return;
        }

        if (node == Last)
        {
            RemoveLast();
            return;
        }

        if (_current == node)
            _current = null;

        Count--;

        node.Next!.Prev = node.Prev;
        node.Prev!.Next = node.Next;
        node.Separate();
    }
    public void Remove(T value)
    {
        Node? del = Find(value);

        if (del == null)
            throw new RemoveException("Item not found");

        Remove(del);
    }

    public void Clear()
    {
        Node? it = First;

        while (it != null)
        {
            Node next = it.Next!;
            it.Separate();
            it = next;
        }

        First = Last = _current = null;
        Count = 0;
    }


    public bool IsEmpty()
    {
        return (Count == 0);
    }

    public Node? Find(T value)
    {
        return Find(i => i.Equals(value));
    }
    public Node? Find(Predicate<T> match)
    {
        if (match == null)
            throw new ArgumentNullException("Predicate is null");

        for (Node? it = First; it != null; it = it.Next)
            if (match(it.Value))
                return it;

        return null;
    }


    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("Index out of collection range.");

            return (index < Count / 2) ? GetElemForward(index) : GetElemBackward(index);
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("Index out of collection array range.");

            if (index < Count / 2)
                SetElemForward(index, value);
            else
                SetElemBackward(index, value);
        }
    }


    public void Reset()
    {
        _current = First;
    }

    public void Next()
    {
        _current = _current?.Next;
    }

    public T RemoveCurrent()
    {
        if (_current == null)
            throw new ArgumentNullException("Invalid iterator");

        T tmp = _current.Value;
        Remove(_current);
        return tmp;
    }


    public IEnumerator<T> GetEnumerator()
    {
        foreach (T value in GetForwardSequence())
            yield return value;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<T> GetForwardSequence()
    {
        int StartVersion = Version;

        for (Node? it = First; it != null; it = it.Next)
        {
            if (StartVersion != Version)
                throw new InvalidOperationException("List was modified after the enumerator was instantiated.");

            yield return it.Value;
        }
    }

    public IEnumerable<T> GetBackwardSequence()
    {
        int StartVersion = Version;

        for (Node? it = Last; it != null; it = it.Prev)
        {
            if (StartVersion != Version)
                throw new InvalidOperationException("List was modified after the enumerator was instantiated.");

            yield return it.Value;
        }
    }



    private T GetElemForward(int index)
    {
        int i = 0;

        for (Node? it = First; it != null; it = it.Next, i++)
            if (i == index)
                return it.Value;

        throw new ArgumentOutOfRangeException("Index out of range");
    }
    private T GetElemBackward(int index)
    {
        int i = Count - 1;

        for (Node? it = Last; it != null; it = it.Prev, i--)
            if (i == index)
                return it.Value;

        throw new ArgumentOutOfRangeException("Index out of range");
    }

    private void SetElemForward(int index, T value)
    {
        int i = 0;

        for (Node? it = First; it != null; it = it.Next, i++)
            if (i == index)
                it.Value = value;

        throw new ArgumentOutOfRangeException("Index out of range");
    }
    private void SetElemBackward(int index, T value)
    {
        int i = Count;

        for (Node? it = Last; it != null; it = it.Prev, i--)
            if (i == index)
                it.Value = value;

        throw new ArgumentOutOfRangeException("Index out of range");
    }
}