using _153501_Abramovich_Lab5.Domain;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Serialization;

public class Serializer : ISerializer<HeatingSystem>
{
    public IEnumerable<HeatingSystem> DeSerializeByLINQ(string fileName)
    {
       var systems = XDocument.Load(fileName)?.
                          Element("ArrayOfHeatingSystems")?.
                          Elements("HeatingSystem")?.
                          Select(hs => ElementToHSystem(hs)) ??
                              throw new InvalidOperationException("File not correct");

        foreach (HeatingSystem hs in systems)
            yield return hs;


        HeatingSystem ElementToHSystem(XElement elem)
        {
            var houses = elem.Elements("House");
            XElement? townElem = elem.Element("Town");
            XElement? priceElem = elem.Element("PricePerSquareMeter");

            int hCount = houses.Count();

            if (townElem == null || priceElem == null ||
                hCount == 0 || elem.Elements().Count() > hCount + 2)
                throw new InvalidOperationException("File not correct");

            var housesList = new List<House>();

            foreach (XElement house in houses)
                housesList.Add(ElementToHouse(house));

            if (!double.TryParse(priceElem.Value, out double price))
                throw new InvalidOperationException("File not correct");

            HeatingSystem hs = new HeatingSystem(townElem.Value, price);

            hs.AddHousesRange(housesList);

            return hs;
        }

        House ElementToHouse(XElement elem)
        {
            XElement? adresElem = elem.Element("Adress");
            XElement? squareElem = elem.Element("Square");

            if(adresElem == null || squareElem == null || elem.Elements().Count() > 2)
                throw new InvalidOperationException("File not correct");

            Adres adres = ElementToAdress(adresElem);

            if (!double.TryParse(squareElem.Value, out double number))
                throw new InvalidOperationException("File not correct");

            return new House(adres, number);
        }

        Adres ElementToAdress(XElement elem)
        {
            XElement? townElem = elem.Element("Town");
            XElement? streetElem = elem.Element("Street");
            XElement? numberElem = elem.Element("Number");

            if (townElem == null || streetElem == null || 
                numberElem == null || elem.Elements().Count() > 3)
                    throw new InvalidOperationException("File not correct");

            if(!int.TryParse(numberElem.Value, out int number))
                throw new InvalidOperationException("File not correct");

            return new Adres(townElem.Value, streetElem.Value, number);
        }
    }

    public IEnumerable<HeatingSystem> DeSerializeJSON(string fileName)
    {
        var jsonFormatter = new DataContractJsonSerializer(typeof(HeatingSystem[]));

        using (var fs = new FileStream(fileName, FileMode.OpenOrCreate)) 
        {
            if (jsonFormatter.ReadObject(fs) is HeatingSystem[] newHsystems)
            {
                foreach (var hsystem in newHsystems)
                    yield return hsystem;
            }
            else
                throw new InvalidOperationException("Uncorrect file");
        }
    }

    public IEnumerable<HeatingSystem> DeSerializeXML(string fileName)
    {
        var xmlFormatter = new XmlSerializer(typeof(HeatingSystem[]));

        using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            if (xmlFormatter.Deserialize(fs) is HeatingSystem[] newHsystems)
            {
                foreach (var hsystem in newHsystems)
                    yield return hsystem;
            }
            else
                throw new InvalidOperationException("Uncorrect file");
        }
    }


    public void SerializeByLINQ(IEnumerable<HeatingSystem> col, string fileName)
    {
        var root = new XElement("ArrayOfHeatingSystems");

        foreach (var hs in col)
            root.Add(HSystemToElement(hs));

        XDocument xdoc = new XDocument(root);

        xdoc.Save(fileName);


        XElement HSystemToElement(HeatingSystem hs)
        {
            var elem = new XElement("HeatingSystem");

            foreach (House h in hs.Houses)
                elem.Add(HouseToElement(h));

            elem.Add(new XElement("Town", hs.Town));
            elem.Add(new XElement("PricePerSquareMeter", hs.PricePerSquareMeter));

            return elem;
        }

        XElement HouseToElement(House h)
        {
            return new XElement("House",
                                 new XElement("Adress",
                                     new XElement("Town", h.Town),
                                     new XElement("Street", h.Street),
                                     new XElement("Number", h.Number)),
                                 new XElement("Square", h.Square));
        }
    }

    public void SerializeJSON(IEnumerable<HeatingSystem> col, string fileName)
    {
        var jsonFormatter = new DataContractJsonSerializer(typeof(HeatingSystem[]));

        using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            jsonFormatter.WriteObject(fs, col.ToArray());
        }
    }

    public void SerializeXML(IEnumerable<HeatingSystem> col, string fileName)
    {
        var xmlFormatter = new XmlSerializer(typeof(HeatingSystem[]));

        using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            xmlFormatter.Serialize(fs, col.ToArray());
        }
    }  
}