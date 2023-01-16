using Collections;
using System.Collections.Generic;

namespace Entities;


public record Tarif(int pricePerMinute);

public record Call(string town, int timeInMinutes, Abonent abonent, Tarif tarif)
{
    public int GetPrice() => tarif.pricePerMinute * timeInMinutes;  
}

public record Abonent(string surname, string town) 
{ 
    public int allPrice { get; internal set; }
}


public class ATS
{
    public CustomCollection<Abonent> _abonents;
    public CustomCollection<Tarif> _tarifs;

    public ATS()
    {
        _abonents = new CustomCollection<Abonent>();
        _tarifs = new CustomCollection<Tarif>();
    }

    public void AddAbonent(Abonent abonent)
    {
        _abonents.Add(abonent);
    }
    public void AddTarif(Tarif tarif)
    {
        _tarifs.Add(tarif);
    }

    public void RemoveAbonent(Abonent abonent)
    {    
        _abonents.Remove(abonent);
    }
    public void RemoveTarif(Tarif tarif)
    {
        _tarifs.Remove(tarif);
    }

    public Abonent GetAbonent(int index)
    {
        return _abonents[index - 1];
    }
    public Tarif GetTarif(int index)
    {
        return _tarifs[index - 1];
    }

    public void RegisterCall(Call call)
    {
        Abonent abonent = _abonents!.Find(call.abonent)?.Value!;

        if (abonent != null)
            abonent.allPrice += call.GetPrice();
    }

    public IEnumerable<Abonent> GetAbonentsSequence()
    {
        foreach(var abonent in _abonents)
            yield return abonent;
    }

    public IEnumerable<Tarif> GetTarifsSequence()
    {
        foreach (var tarif in _tarifs)
            yield return tarif;
    }
}