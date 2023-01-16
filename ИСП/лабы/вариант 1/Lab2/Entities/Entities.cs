using Collections;



namespace Entities;


public record Abonent(string surname, string town);

public record Tarif(decimal pricePerMinute, decimal discount);


public class ATS
{
    public enum Change
    {
        Add,
        Remove,
        Call,
    }

    private CustomCollection<Abonent> _abonents;
    private CustomCollection<Tarif> _tarifs;


    public event Action<object, Change>? ListChanged;

    public event Action<Abonent, Tarif>? CallingAbonent;

    public ATS()
    {
        _abonents = new CustomCollection<Abonent>();
        _tarifs = new CustomCollection<Tarif>();
    }

    public int AbonentsCount
    {
        get => _abonents.Count; 
    }
    public int TarifsCount
    {
        get => _tarifs.Count; 
    }


    public void AddAbonent(Abonent abonent)
    {
        _abonents.Add(abonent);
        ListChanged?.Invoke(abonent, Change.Add);
    }
    public void AddTarif(Tarif tarif)
    {
        _tarifs.Add(tarif);
        ListChanged?.Invoke(tarif, Change.Add);
    }

    public void RemoveAbonent(Abonent abonent)
    {
        try
        {
            _abonents.Remove(abonent);
            ListChanged?.Invoke(abonent, Change.Remove);
        }
        catch(RemoveException)
        {
            Console.WriteLine($"  {abonent} not found.");
        }
    } 
    public void RemoveTarif(Tarif tarif)
    {
        _tarifs.Remove(tarif);
        ListChanged?.Invoke(tarif, Change.Remove);
    }

    public Abonent GetAbonent(int index)
    {
        return _abonents[index - 1];    
    }
    public Tarif GetTarif(int index)
    {
        return _tarifs[index - 1];
    }

    public void CallTo(Abonent abonent, Tarif tarif)
    {
        CallingAbonent?.Invoke(abonent, tarif);
    }
}

public class Journal
{
    private CustomCollection<string> _records;

    public Journal()
    {
        _records = new CustomCollection<string>();
    }

    public void AddRecord(object recordObject, ATS.Change change)
    {
        _records.Add($"  Owner {change} {recordObject}");
    }

    public void PrintRecords()
    {
        foreach(var record in _records)
            Console.WriteLine(record);
    }
}

