namespace Entities;


[Serializable]
public class Apartment
{
    public Apartment() { }

    public Apartment(int id, string name, int tenantsCount)
    {
        Id = id;
        Name = name;
        TenantsCount = tenantsCount;
    }

    public int? Id { get; init; }

    public string? Name { get; init; }

    public int? TenantsCount { get; init; }
}


