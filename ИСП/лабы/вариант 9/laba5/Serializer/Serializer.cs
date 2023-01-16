using System;
using System.Collections.Generic;
using laba5.Domain;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

namespace Serializer
{
    public class Serializer : ISerializer
    {
        public IEnumerable<Computer> DeSerializeByLINQ(string fileName)
        {
            XDocument xdoc = XDocument.Load(fileName);
            XElement root = xdoc.Root;
            if (root is not null)
            {
                foreach (XElement element in root.Elements())
                {
                    XElement temp = element.Element("monitor;
                    XElement Name = temp.Element("Name");

                    yield return new(Name.Value);
                }
            }
        }

        public IEnumerable<Computer> DeSerializeJSON(string fileName)
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

            List<Computer> elements = JsonConvert.DeserializeObject<List<Computer>>(obj);
            foreach (var element in elements)
                yield return element;

            //using (FileStream fileStream = new(fileName, FileMode.OpenOrCreate))
            //    while (fileStream.CanRead)
            //        yield return await JsonSerializer.DeserializeAsync<Computer>(fileStream);
        }

        public IEnumerable<Computer> DeSerializeXML(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File {fileName} wasn't found!!!");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Computer>));
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var elements = xmlSerializer.Deserialize(fileStream) as List<Computer>;
                foreach (var element in elements)
                    yield return element;               
            }
        }

        public void SerializeByLINQ(IEnumerable<Computer> data, string fileName)
        {
            if(File.Exists(fileName))
                File.Delete(fileName);

            XDocument xdoc = new();
            XElement root = new("ArrayOfComputer");
            foreach(var element in data)
            {
                XElement element1 = new("Computer");
                XElement element2 = new("monitor");
                XElement element3 = new("Name", element.monitor.Name);
                element2.Add(element3);
                element1.Add(element2);
                root.Add(element1);
            }
            xdoc.Add(root);
            xdoc.Save(fileName);
        }

        public void SerializeJSON(IEnumerable<Computer> data, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream fileStream = File.Create(fileName)) { }

            JsonSerializer jsonSerializer = new();
            using (StreamWriter streamWriter = new(fileName))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                jsonSerializer.Serialize(jsonWriter, data);

            //using (FileStream fileStream = new(fileName, FileMode.OpenOrCreate))
            //    foreach (var element in data)
            //        await JsonSerializer.SerializeAsync<Computer>(fileStream, element);    
        }

        public void SerializeXML(IEnumerable<Computer> data, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream fileStream = File.Create(fileName)) { }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Computer>));

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                xmlSerializer.Serialize(fileStream, data);
        }
    }
}
