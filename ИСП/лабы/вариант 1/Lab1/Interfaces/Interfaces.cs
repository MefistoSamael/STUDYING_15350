namespace Interfaces;

public interface ICustomCollection<T>
{
    T this[int index] { get; set; }

    void Reset();
    void Next();

    T Current { get; }
    int Count { get; }

    void Add(T item);
    void Remove(T item);
    T RemoveCurrent();
}
