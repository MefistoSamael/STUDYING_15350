using lab5.Domain;
using Serializers;

{
    List<Factory> factories = new List<Factory>()
    {
        new Factory("Pup1", new List<Storage>
        {
            new Storage("Lup1InPup1", "Minsk, Levkova 12", 255),
            new Storage("Lup2InPup1", "Brest, Sonic 35", 229),
            new Storage("Lup3InPup1", "Mogilev, Pony 9", 301)
        }),
        new Factory("Pup2", new List<Storage>
        {
            new Storage("Lup1InPup2", "Sho, Centr 3", 90),
            new Storage("Lup2InPup2", "Minsk, Gikalo 11", 350),
        })
    };

    Serializer serializer = new Serializer();
    serializer.SerializeXML(factories, "factories.xml");

    List<Factory> factoriesTest = new List<Factory>(serializer.DeSerializeXML("factories.xml"));
    foreach(Factory fact in factoriesTest)
    {
        Console.WriteLine("\n" + fact.Name);
        fact.AllStorage();
    }
    Console.WriteLine("\nByLINQ");

    serializer.SerializeByLINQ(factories, "factories2.xml");
    factoriesTest = new List<Factory>(serializer.DeSerializeByLINQ("factories2.xml"));

    foreach (Factory fact in factoriesTest)
    {
        Console.WriteLine("\n" + fact.Name);
        fact.AllStorage();
    }

    Console.WriteLine("\nJson");

    serializer.SerializeJSON(factories, "factories3.json");

    factoriesTest = new List<Factory>(serializer.DeSerializeJSON("factories3.json"));

    foreach (Factory fact in factoriesTest)
    {
        Console.WriteLine("\n" + fact.Name);
        fact.AllStorage();
    }
}