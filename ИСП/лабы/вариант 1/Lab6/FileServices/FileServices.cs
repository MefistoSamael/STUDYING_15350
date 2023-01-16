using Entities;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace FileServices;


public class EmployeeFileService : IFileService<Employee>
{
    public string FileName { get; init; }

    public EmployeeFileService(string fileName) 
    {
        FileName = fileName; 
    }

    public IEnumerable<Employee> ReadFile()
    {
        var jsonFormatter = new DataContractJsonSerializer(typeof(Employee[]));

        using (var fs = new FileStream(FileName, FileMode.OpenOrCreate))
        {
            if (jsonFormatter.ReadObject(fs) is Employee[] newEmployes)
            {
                foreach (var e in newEmployes)
                    yield return e;
            }
            else
                throw new InvalidOperationException("Uncorrect file");
        }
    }

    public void SaveData(IEnumerable<Employee> col)
    {
        var jsonFormatter = new DataContractJsonSerializer(typeof(Employee[]));

        using (var fs = new FileStream(FileName, FileMode.OpenOrCreate))
        {
            jsonFormatter.WriteObject(fs, col.ToArray());

//
            //JsonSerializer.ser
        }

        if (FileName is null)
            Console.WriteLine("null");
    }
}

