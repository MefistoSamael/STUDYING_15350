using System;
using System.Collections.Generic;
using System.IO;
using laba4.Interfaces;

namespace laba4.Entities
{
    class FileService : IFileService<Competitor>
    {
        public IEnumerable<Competitor> ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File {fileName} wasn't found!!!");

            using (FileStream fileStream = new(fileName, FileMode.Open, FileAccess.Read)) // создаём поток, связанный с файлом
            using (BinaryReader binaryReader = new(fileStream)) // создаём экземпляр BinaryReader и связываем с предыдущим потоком
                while(binaryReader.PeekChar() > -1)
                {
                    string Name;
                    Int32 Age;
                    bool Winner;

                    try
                    {
                        Name = binaryReader.ReadString();
                        Age = binaryReader.ReadInt32();
                        Winner = binaryReader.ReadBoolean();
                    }
                    catch (EndOfStreamException) // исключение, которое выдается при попытке чтения за концом потока
                    {
                        Console.WriteLine("Something went wrong :(");
                        yield break;
                    }
                    catch (IOException) // исключение, которое выдается при возникновении ошибки ввода-вывода
                    {
                        Console.WriteLine("Something went wrong :(");
                        yield break;
                    }

                    yield return new Competitor(Name, Age, Winner);
                }
        }

        public void SaveData(IEnumerable<Competitor> data, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream fileStream = File.Create(fileName)) { }

            //using (FileStream fileStream = new(fileName, FileMode.OpenOrCreate))
            using (BinaryWriter binaryWriter = new(File.Open(fileName, FileMode.OpenOrCreate)))
                foreach (var element in data)
                {
                    try
                    {
                        binaryWriter.Write(element.Name);
                        binaryWriter.Write(element.Age);
                        binaryWriter.Write(element.Winner);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Something went wrong :(");
                        return;
                    }
                }
        }

        public void RenameFile(string old_fileName, string new_fileName)
        {
            if (!File.Exists(old_fileName))
                throw new FileNotFoundException($"File {old_fileName} wasn't found!!!");
            if (File.Exists(new_fileName))
                File.Delete(new_fileName);

            File.Move(old_fileName, new_fileName);
        }
    }
}
