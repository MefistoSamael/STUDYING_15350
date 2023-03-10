using System.Collections.Generic;
using System.Reflection;

namespace Entities;



public record Tarif(string Name, decimal SubscriptionFeePerMinute);

public record Call(int TimeInMinutes, Tarif Tarif)
{
    public decimal GetPrice() => Tarif.SubscriptionFeePerMinute * TimeInMinutes;
}


public class Abonent
{
    public string Name { get; init; }

    private List<Tarif> _tarifPlan;
    private List<Call> _callList;


    public Abonent(string name)
    {
        Name = name;

        _tarifPlan = new List<Tarif>();
        _callList = new List<Call>();
    }


    internal void AddTarif(Tarif tarif)
    {
        _tarifPlan.Add(tarif);
    } 
    internal void RemoveTarif(Tarif tarif)
    {
        _tarifPlan.Remove(tarif);
    }  


    public bool Contains(Tarif tarif)
    {
        return _tarifPlan.Contains(tarif);
    } 

    internal void AddCall(Call call)
    {
        _callList.Add(call);
    } 

    public IEnumerable<Tarif> TarifsSequence  
    {
        get => _tarifPlan;
    }

    public decimal TotalPrice
    {
        get => _callList.Sum(call => call.GetPrice());
    }

    public IEnumerable<(string, decimal)> TarifPricesSequance()
    {
        if(_callList.Count == 0)
            throw new InvalidOperationException("Abonents never called");

        return _callList.
                GroupBy(call => call.Tarif).
                Select(group => (group.Key.Name, group.
                                                 Sum(call => call.GetPrice())));
    }

    public override string ToString()
    {
        return $"Abonent with name {Name}";
    }
}

public class Ate
{
    public string Town { get; init; }

    private Dictionary<string, Tarif> _tarifs;
    private List<Abonent> _abonents;

    public enum Change
    {
        Add,
        Remove
    }

    public event Action<Change, object>? ListsChange;
    public event Action<string, string>? CallingAbonent;


    public Ate(string town)
    {
        Town = town;

        _tarifs = new Dictionary<string, Tarif>();    
        _abonents = new List<Abonent>();    
    }


    public void AddAbonents(params Abonent[] abonents) 
    {
        var selection = abonents.Select(a => a.Name);

        if(selection.
           Distinct().
           Count() < abonents.Length)
                throw new InvalidOperationException("Same abonents in arguments");

        if (selection.
            Intersect(AbonentNamesSequence).
            Count() > 0)
                throw new InvalidOperationException("Same abonents is already registered on this ATE");

        _abonents.EnsureCapacity(_abonents.Count + abonents.Length);

        foreach (Abonent abonent in abonents)
        {
            _abonents.Add(abonent);
            ListsChange?.Invoke(Change.Add, abonent);
        }
    }

    public void RemoveAbonent(string name) 
    {
        Abonent? abonent = FindAbonent(name);

        if(abonent == null)
            throw new InvalidOperationException("Abonent not found");

        _abonents.Remove(abonent);

        ListsChange?.Invoke(Change.Remove, abonent);
    }  


    public void AddTarifs(params Tarif[] tarifs)  
    {
        try
        {
            _tarifs = new(_tarifs.
                          Union(tarifs.
                                Select(tarif => new KeyValuePair<string, Tarif>(tarif.Name, tarif))));
        }
        catch (ArgumentException)
        {
            throw new InvalidOperationException("Same tarif is already registered on the ATE");
        }

        foreach(Tarif tarif in tarifs)
            ListsChange?.Invoke(Change.Add, tarif);
    }

    public void RemoveTarif(string tarifName)   
    {
        Tarif? tarif = FindTarif(tarifName);

        if(tarif == null)
            throw new InvalidOperationException("Tarif not found");

        _tarifs.Remove(tarifName);

        ListsChange?.Invoke(Change.Remove, tarif);
    }


    public Tarif? FindTarif(string name) 
    {
        try
        {
            return _tarifs[name];
        }
        catch(KeyNotFoundException)
        {
            return null;
        }
    }

    public Abonent? FindAbonent(string name)  
    {
        return _abonents.Find(abonent => abonent.Name == name);
    }


    public void AddTarifTo(string abonentName, string tarifName) 
    {
        Abonent? abonent = FindAbonent(abonentName);
        Tarif? tarif = FindTarif(tarifName);

        if(abonent == null)
            throw new InvalidOperationException("Abonent is not founded");

        if (tarif == null)
            throw new InvalidOperationException("Tarif is not founded");

        if (abonent.Contains(tarif))
            throw new InvalidOperationException("Abonent already subscribed to this tarif");

        abonent.AddTarif(tarif);
    }

    public void RemoveTarifFrom(string abonentName, string tarifName) 
    {
        Abonent? abonent = FindAbonent(abonentName);
        Tarif? tarif = FindTarif(tarifName);

        if (abonent == null)
            throw new InvalidOperationException("Abonent is not founded");

        if (tarif == null)
            throw new InvalidOperationException("Tarif is not founded");

        if (!abonent.Contains(tarif))
            throw new InvalidOperationException("Abonent is not subscribed to this tarif");

        abonent.RemoveTarif(tarif);
    }


    public void RegisterCall(string abonentName, string tarifName, int timeInMinutes) 
    {
        Abonent? abonent = FindAbonent(abonentName);
        Tarif? tarif = FindTarif(tarifName);

        if (abonent == null)
            throw new InvalidOperationException("Abonent is not founded");

        if (tarif == null || !abonent.Contains(tarif))
            throw new InvalidOperationException("Abonent is not subscribed to this tarif");

        abonent.AddCall(new Call(timeInMinutes, tarif));

        CallingAbonent?.Invoke(abonentName, tarifName);
    }

    public IEnumerable<Tarif> TarifsSequence   
    {
        get => _tarifs.
               OrderBy(t => t.Value.SubscriptionFeePerMinute).
               Select(t => t.Value);
    }

    public IEnumerable<string> TarifsNamesSequence   
    {
        get => TarifsSequence.
               Select(tarif => tarif.Name);
    }

    public IEnumerable<Abonent> AbonentSequence   
    {
        get => _abonents.
               OrderBy(a => a.Name);
    }

    public IEnumerable<string> AbonentNamesSequence   
    {
        get => AbonentSequence.
               Select(abonent => abonent.Name);
    }


    public int TarifsCount  
    {
        get => _tarifs.Count;
    }
    public int AbonentsCount  
    {
        get => _abonents.Count;
    }

    public decimal TotalPrice 
    {
        get => _abonents.Sum(abonent => abonent.TotalPrice);
    }

    public string PrincipalDebtorName() 
    {
        if (_abonents.Count == 0)
            throw new InvalidOperationException("No one is registered on ATE");

        return _abonents.
                Aggregate(_abonents[0], (max, next) => max.TotalPrice > next.TotalPrice
                                                                        ? max
                                                                        : next).Name;    
    }

    public int PaidMoreCount(decimal price) 
    {
        if (_abonents.Count == 0)
            throw new InvalidOperationException("No one is registered on ATE");

        return _abonents.
               Aggregate(0, (total, next) => next.TotalPrice > price
                                                             ? total + 1
                                                             : total);
    }
}

public class Journal
{
    private List<string> _records;

    public Journal()
    {
        _records = new List<string>();
    }

    public void AddRecord(Ate.Change change, object recordObject)
    {
        _records.Add($"  Owner {change} {recordObject}");
    }

    public void PrintRecords()
    {
        foreach (var record in _records)
            Console.WriteLine(record);
    }
}










