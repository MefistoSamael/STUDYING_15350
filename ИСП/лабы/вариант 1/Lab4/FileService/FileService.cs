using Entities;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Xml.Linq;


namespace FileServices;

public interface IFileService<T>
{
    IEnumerable<T> ReadFile(string fileName);

    void SaveData(IEnumerable<T> data, string fileName);
}


public class FileService : IFileService<Tenant>
{
    public IEnumerable<Tenant> ReadFile(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException($"File {fileName} not found");

        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(fs))      
            while (reader.PeekChar() > -1)
            { 
                string name;
                int apartmentNumber;

                try
                {
                    name = reader.ReadString();
                    apartmentNumber = reader.ReadInt32();
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("File is not correct");
                    yield break;
                }
                catch(IOException)
                {
                    Console.WriteLine("IOException");
                    yield break;
                }

                yield return new Tenant(name, apartmentNumber);
            }
    }

    public void SaveData(IEnumerable<Tenant> tenants, string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException($"File {fileName} not found");

        using (FileStream fs = new FileStream(fileName, FileMode.Open))  
        using (BinaryWriter writer = new BinaryWriter(fs))
            foreach(Tenant tenant in tenants)
            {
                try
                {
                    writer.Write(tenant.Name ?? "Undefined");
                    writer.Write(tenant.ApartmentNumber);
                }
                catch (IOException)
                {
                    Console.WriteLine("IOException");
                    return;
                }
            }
    }

    public void RenameFile(string oldName, string newName)
    {
        if(oldName == null || newName == null)
            throw new ArgumentNullException("Argument is null");

        if (!File.Exists(oldName))
            throw new FileNotFoundException($"File {oldName} not found");

        File.Move(oldName, newName);
    }

}