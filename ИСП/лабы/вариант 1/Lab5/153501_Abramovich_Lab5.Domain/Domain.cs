using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;

namespace _153501_Abramovich_Lab5.Domain;

[Serializable]
[DataContract]
public class Adres
{
    [DataMember]
    public string Town { get; init; }
    [DataMember]
    public string Street { get; init; }
    [DataMember]
    public int Number { get; init; }

    public Adres(string town, string street, int number)
    {
        Town = town;
        Street = street;
        Number = number;
    }

    public Adres()
    {
        Town = "Undefined";
        Street = "Undefined";
        Number = 0;
    }

    public override string ToString()
    {
        return $"{Town} {Street} {Number}";
    }
}

[Serializable]
[DataContract]
public class House
{
    [DataMember]
    public Adres Adres { get; init; }
    [DataMember]
    public double Square { get; init; }

    public House(Adres adres, double square)
    {
        Adres = adres;
        Square = square;
    }

    public House()
    {
        Adres = new Adres();
        Square = 0;
    }

    public string Town
    {
        get => Adres.Town;
    }

    public string Street
    {
        get => Adres.Street;
    }

    public int Number
    {
        get => Adres.Number;
    }
}

[Serializable]
[DataContract]
public class HeatingSystem
{
    [DataMember]
    public List<House> Houses;

    [DataMember]
    public string Town { get; init; }
    [DataMember]
    public double PricePerSquareMeter { get; init; }
   

    public HeatingSystem(string town, double pricePerSquareMeter)
    {
        Houses = new List<House>();

        Town = town;
        PricePerSquareMeter = pricePerSquareMeter;
    }

    public HeatingSystem() : this("Undefined",0)
    { 
    }

    public void AddHouse(House house)
    {
       if(house.Town != Town)
            throw new InvalidDataException("House located in other town.");

       if(Houses.Select(h => h.Adres).
                  Contains(house.Adres))
                      throw new InvalidDataException("House with this adress already added.");

        Houses.Add(house);
    }

    public void AddHousesRange(IEnumerable<House> col)
    {
        foreach (House house in col)
            AddHouse(house);
    }

    public double GetTotalPrice()
    {
        return Houses.Sum(house => house.Square) * PricePerSquareMeter;
    }

    public double GetPrisePerHouse(Adres adres)
    {
        if(adres.Town != Town)
            throw new InvalidDataException("This heating system located in other town.");

        House? house = Houses.Find(h => h.Adres == adres);

        if (house == null)
            throw new InvalidDataException("House with this adress not added.");

        return house.Square * PricePerSquareMeter;
    }
    public double GetPrisePerHouse(string street, int number)
    {
        return GetPrisePerHouse(new Adres(this.Town, street, number));
    }

    public double GetPrisePerStreet(string street)
    {
        var houses = Houses.Where(h => h.Street == street);

        if (houses.Count() == 0)
            throw new InvalidDataException("Houses with this street not added.");

        return houses.Sum(h => h.Square) * PricePerSquareMeter;
    }
}


public interface ISerializer<T>
{
    IEnumerable<T> DeSerializeByLINQ(string fileName);
    IEnumerable<T> DeSerializeXML(string fileName);
    IEnumerable<T> DeSerializeJSON(string fileName);

    void SerializeByLINQ(IEnumerable<T> col, string fileName);
    void SerializeXML(IEnumerable<T> col, string fileName);
    void SerializeJSON(IEnumerable<T> col, string fileName);
}

