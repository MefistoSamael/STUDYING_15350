using Entities;

var ate = new Ate("Minsk");

var journal = new Journal();

ate.ListsChange += journal.AddRecord;
ate.CallingAbonent += (abonentName, tarifName) => Console.WriteLine($" {abonentName} call with {tarifName}");

ate.AddTarifs(new Tarif("1t", 10), new Tarif("2t", 20), new Tarif("3t", 30));
ate.AddAbonents(new("Lolev"), new("Lalkin"), new("Lylkin"));
ate.AddAbonents(new Abonent("Lolkin"));

//ate.RemoveAbonent("Lolkin");

ate.AddTarifs(new Tarif("mini", 10m), new("medival", 15m), new("3jt", 20m));

ate.AddTarifTo("Lolkin", "1t");
ate.AddTarifTo("Lolkin", "2t");
ate.AddTarifTo("Lolkin", "3t");

ate.RegisterCall("Lolkin", "2t", 20);

ate.RegisterCall("Lolkin", "1t", 5);
ate.RegisterCall("Lolkin", "1t", 10);

ate.RegisterCall("Lolkin", "3t", 10);
ate.RegisterCall("Lolkin", "3t", 10);
ate.RegisterCall("Lolkin", "3t", 10);

foreach(var a in ate.FindAbonent("Lolkin").TarifPricesSequance())
    Console.WriteLine(a.Item1 + "   " + a.Item2);

Console.WriteLine(ate.PaidMoreCount(200m));

journal.PrintRecords();

//ate.AddAbonents(new Abonent("Lylkin"));

//ate.PrintTarifs();

