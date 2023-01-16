using Collections;
using Entities;


class Program
{
    static void Main(string[] args)
    {
        ATS ats = new ATS();
        Journal journal = new Journal();

        ats.ListChanged += journal.AddRecord;
        ats.CallingAbonent += (abonent, tarif) =>  Console.WriteLine($"  Owner {ATS.Change.Call} {abonent} with {tarif}"); 

        ats.AddAbonent(new Abonent("Lolev", "Lolinsk"));
        ats.AddAbonent(new Abonent("Hihirov", "Hihihansk"));
        ats.AddAbonent(new Abonent("Gygyrovich", "Gagomel"));

        ats.AddTarif(new Tarif(0.1m, 0.9m));
        ats.AddTarif(new Tarif(0.2m, 0.8m));
        ats.AddTarif(new Tarif(0.3m, 0.7m));

       // ats.RemoveAbonent(new Abonent("Lole", "Lolinsk"));

       // ats.CallTo(ats.GetAbonent(0), ats.GetTarif(0));

        journal.PrintRecords();
    }
}