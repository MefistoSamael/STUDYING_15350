using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    //класс реализующий работу с файлом
    internal class FileService : IFileService<Employee>
    {
        //метод чтения из файла
        public IEnumerable<Employee> ReadFile(string fileName)
        {
            //проверка
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File {fileName} not found");

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name;
                        int id;

                        try
                        {
                            name = reader.ReadString();
                            id = reader.ReadInt32();
                        }
                        catch (EndOfStreamException)
                        {
                            Console.WriteLine("File is not correct");
                            yield break;
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            yield break;
                        }

                        yield return new Employee(id, name);
                    }
                }
            }
        }

        public void SaveData(IEnumerable<Employee> data, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    foreach (Employee employee in data)
                    {
                        try
                        {
                            writer.Write(employee.Name);
                            writer.Write(employee.Id);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            return;
                        }
                    }
                }
            }
        }
    }
}
