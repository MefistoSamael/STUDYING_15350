using System;
using System.Collections.Generic;
using System.IO;
using laba6;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public class FileService : IFileService<Employee>
    {
        public IEnumerable<Employee> ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File {fileName} wasn't found!!!");

            JsonSerializer jsonSerializer = new();
            string obj = " ";
            using (StreamReader fileStream = new StreamReader(fileName))
                while (true)
                {
                    string temp = fileStream.ReadLine();
                    if (temp == null) break;
                    obj += temp;
                }

            List<Employee> elements = JsonConvert.DeserializeObject<List<Employee>>(obj);
            foreach (var element in elements)
                yield return element;           
        }

        public void SaveData(IEnumerable<Employee> data, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream fileStream = File.Create(fileName)) { }

            JsonSerializer jsonSerializer = new();
            using (StreamWriter streamWriter = new(fileName))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                jsonSerializer.Serialize(jsonWriter, data);
        }
    }
}
