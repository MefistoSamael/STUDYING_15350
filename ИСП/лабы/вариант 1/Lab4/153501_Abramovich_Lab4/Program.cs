
using Entities;
using FileServices;
using System.Text;



class Lab_4
{
    static void Main()
    {
        //string path = @"C:\Users\HP\Desktop\Текстовые документы\tst.dat";

        var tenants = new List<Tenant>() 
        { 
            new("Peter", 2), 
            new("Kate", 34),
            new("Ronald", 9),
            new("John", 18),
            new("Alex", 42),
        };

        FileService fs = new();

        fs.SaveData(tenants, "tst.dat");

        fs.RenameFile("tst.dat", "Tst.dat");


        var newTenants = fs.ReadFile("Tst.dat");

        var newTenantsSorted1 = newTenants.OrderBy(t => t, new TenantComparer());

        var newTenantsSorted2 = newTenants.OrderBy(t => t.ApartmentNumber);

        Console.WriteLine("\nОригинал\n");

        foreach (var tent in newTenants)
            Console.WriteLine(tent.Name + "   " + tent.ApartmentNumber);

        Console.WriteLine("\nСортировка компаратором\n");

        foreach (var tent in newTenantsSorted1)
            Console.WriteLine(tent.Name + "   " + tent.ApartmentNumber);

        Console.WriteLine("\nСортировка через лямбда-выражение по номеру квартиры\n");

        foreach (var tent in newTenantsSorted2)
            Console.WriteLine(tent.Name + "   " + tent.ApartmentNumber);
    }
}