
using _153501_Abramovich_Lab5.Domain;
using System.Xml.Serialization;
using Serialization;

class Program
{ 
    static void Main()
    {
        string fileNameXml = "systems.xml";
        string fileNameJson = "systems.json";
        string fileNameLinq = "systemsLINQ.xml";

        HeatingSystem Hs1 = new HeatingSystem("Minsk", 100);

        Hs1.AddHouse(new House(new Adres("Minsk", "levkova", 5), 32));
        Hs1.AddHouse(new House(new Adres("Minsk", "krasivaya", 9), 26));

        HeatingSystem Hs2 = new HeatingSystem("Mogiliov", 90);

        Hs2.AddHouse(new House(new Adres("Mogiliov", "sinaya", 48), 83));
        Hs2.AddHouse(new House(new Adres("Mogiliov", "velikaya", 1), 40));

        var HsList = new List<HeatingSystem>(2) { Hs1, Hs2 };

        Serializer s = new Serializer();


        s.SerializeXML(HsList.ToArray(), fileNameXml);
        var newHsystems = s.DeSerializeXML(fileNameXml);

        foreach (HeatingSystem hs in newHsystems)
            foreach (House h in hs.Houses)
                Console.WriteLine(h.Adres);

        Console.WriteLine();


        s.SerializeJSON(HsList.ToArray(), fileNameJson);
        newHsystems = s.DeSerializeJSON(fileNameJson);

        foreach (HeatingSystem hs in newHsystems)
            foreach (House h in hs.Houses)
                Console.WriteLine(h.Adres);

        Console.WriteLine();

        s.SerializeByLINQ(HsList.ToArray(), fileNameLinq);
        newHsystems = s.DeSerializeByLINQ(fileNameLinq);

        foreach (HeatingSystem hs in newHsystems)
            foreach (House h in hs.Houses)
                Console.WriteLine(h.Adres);
    }
}


