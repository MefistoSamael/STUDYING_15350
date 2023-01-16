namespace Entities;

public record Tenant(string Name, int ApartmentNumber);


public class TenantComparer : IComparer<Tenant>
{
    public int Compare(Tenant? first, Tenant? second)
    {
        if (first == null && second == null)
            return 0;

        if (first == null)
            return -1;

        if (second == null)
            return 1;

        return first!.Name.CompareTo(second!.Name);
    }
}
