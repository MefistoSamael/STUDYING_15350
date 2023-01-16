using Entities;


public class Program
{
    private static Random _rand = new Random(60);


    static async Task Main(string[] args)
    {
        Progress progres = new Progress() { };

        progres.Action += percent => Console.Write($"\rПоток:\t {Thread.CurrentThread.ManagedThreadId} \t Запись в поток:\t {percent}% ");


        StreamService<Apartment> service = new StreamService<Apartment>("Data.json");

        MemoryStream mStream = new MemoryStream();

        Apartment[] apartments = new Apartment[1000];

        for (int i = 0; i < 1000; i++)
        {
            apartments[i] = GetRandomApartment();
        }

        var task1 = service.WriteToStreamAsync(mStream, apartments, progres);

        await Task.Delay(100);

        var task2 = service.CopyFromStreamAsync(mStream, progres);

        task1.Wait();
        task2.Wait();

        Task<int> getStatiscticsTask = service.GetStatisticsAsync(x => x.TenantsCount > 100);
        int apartmensCount = await getStatiscticsTask;

        Console.WriteLine("GetStatisticsAsync вернул результат: " + apartmensCount);
    }

    static Apartment GetRandomApartment()
    {
        return new Apartment
        {
            Id = (int)DateTime.Now.Ticks,
            Name = ((DayOfWeek)_rand.Next(1, 7)).ToString(),
            TenantsCount = _rand.Next(1, 400),
        };
    }
}