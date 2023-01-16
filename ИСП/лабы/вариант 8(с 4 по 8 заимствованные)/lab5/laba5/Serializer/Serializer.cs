using lab5.Domain;
using System.Xml.Serialization;
using System;
using System.Xml.Linq;
using System.Text.Json;

namespace Serializers
{
    public class Serializer : ISerializer
    {
        public IEnumerable<Factory> DeSerializeByLINQ(string fileName)
        {
            List<Factory> list = new List<Factory>();
            //загружаем документ по имени
            XDocument? xdoc = XDocument.Load(fileName);
            if (xdoc == null)
                throw new Exception("Некорректный файл");
            XElement root = xdoc.Root!;
            //проходим по всем заводам
            foreach (XElement factory in root.Elements("Factory"))
            {
                if(factory == null)
                    throw new Exception("Некорректный файл");
                //берем название завода
                string name = factory.Element("Name")!.Value;
                List<Storage> storages = new List<Storage>();

                XElement Xstorages = factory!.Element("Storages")!;
                foreach (XElement storage in Xstorages.Elements("Storage"))
                {
                    if(storage == null)
                        throw new Exception("Некорректный файл");
                    //создаем объект класса склад
                    Storage stor = new Storage(storage.Element("Name")!.Value,
                        storage.Element("Address")!.Value,
                       Convert.ToInt32(storage.Element("Roominess")!.Value));
                    //добавляем склад в список складов
                    storages.Add(stor);
                }
                //добавляем завод в списко заводов
                list.Add(new Factory(name, storages));
            }
            return list;
        }

        public IEnumerable<Factory> DeSerializeJSON(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                List<Factory>? list = JsonSerializer.Deserialize<List<Factory>>(fs);                                          
                //                                             ↑
                //                                             |
                //                                             |
                //если не удалось произвести десереализацию, то метод выше вернет null
                if (list == null)
                    throw new Exception("Некорректный файл");
                return list;
            }
        }

        public IEnumerable<Factory> DeSerializeXML(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Factory>));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                List<Factory>? list = formatter.Deserialize(fs) as List<Factory>;
                //Deserialize вернет object и мы его пытаемся преобразовать, при помощи as,
                //в List<Factory> 
                //если не получится - то as вернет Null
                if (list == null)
                    throw new Exception("Некорректный файл");
                return list;
            }
        }

        public void SerializeByLINQ(IEnumerable<Factory> factory, string fileName)
        {
            //создаем файл для сохранения сериализации
            XDocument xdoc = new XDocument();
            //корень
            XElement Root = new XElement("ArrayFactory");
            
            //перебираем коллекцию заводов
            foreach (Factory fact in factory)
            {
                //создаем тег завод
                XElement factor = new XElement("Factory");
                //к тегу завод добаляем тег ИмяЗавода
                factor.Add(new XElement("Name", fact.Name));
                //создаем тег в котором будем хранить отдельные склады
                XElement storages = new XElement("Storages");

                //пребираем склады, принадлежащие заводу
                foreach (Storage storage in fact.Storages)
                {
                    //создаем тег скалада
                    XElement stor = new XElement("Storage");
                    //добавляем в него следующие теги:
                    stor.Add(new XElement("Name", storage.Name));
                    stor.Add(new XElement("Address", storage.Address));
                    stor.Add(new XElement("Roominess", storage.Roominess));
                    storages.Add(stor);
                }
                //добавляем тег складов в тег завода
                factor.Add(storages);
                //добавляем тег завода в корень
                Root.Add(factor);
            }
            //добавляем корень в документ
            xdoc.Add(Root);
            //сохраняем документ
            xdoc.Save(fileName);
        }

        public void SerializeJSON(IEnumerable<Factory> factory, string fileName)
        {

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.SerializeAsync<IEnumerable<Factory>>(fs, factory, options);
            }
        }

        public void SerializeXML(IEnumerable<Factory> factory, string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Factory>));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, factory);
            }
        }
    }
}