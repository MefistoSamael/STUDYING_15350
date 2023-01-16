using System.Runtime.Serialization;

namespace Entities;


[DataContract]
public class Employee
{
    [DataMember]
    public int Id { get; init; }
    [DataMember]
    public string Name { get; init; }

    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"Id = {Id}  Name = {Name}";
    }
}

public interface IFileService<T> where T : class
{
    IEnumerable<T> ReadFile();

    void SaveData(IEnumerable<T> col);

    string FileName { get; init; }
}
