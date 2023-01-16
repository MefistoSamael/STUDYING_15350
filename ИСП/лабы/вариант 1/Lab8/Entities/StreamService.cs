using System.Text;
using System.Text.Json;

namespace Entities;


public class StreamService<T>
{
    public string FileName { get; set; }

    private Semaphore _sem = new Semaphore(1, 1);

    public StreamService(string fileName)
    {
        FileName = fileName;
    }


    public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> col, IProgress<int> prog)
    {
        _sem.WaitOne();

        Console.WriteLine($"\nЗапись началась в поток: {Thread.CurrentThread.ManagedThreadId}");

        int count = 0;
        await stream.WriteAsync(Encoding.ASCII.GetBytes("["));

        foreach (var item in col)
        {
            if (count != 0) 
                await stream.WriteAsync(Encoding.ASCII.GetBytes(","));

            count++;

            //await Task.Delay(200);

            prog.Report(count * 100 / col.Count());
            await JsonSerializer.SerializeAsync(stream, item);
        }

        await stream.WriteAsync(Encoding.ASCII.GetBytes("]"));

        stream.Position = 0;
        Console.WriteLine($"\nЗапись завершена в поток: {Thread.CurrentThread.ManagedThreadId}\n");

        _sem.Release();
    }

    public async Task CopyFromStreamAsync(Stream stream, IProgress<int> prog)
    {
        _sem.WaitOne();

        Console.WriteLine($"\nКопирование началось в поток: {Thread.CurrentThread.ManagedThreadId}");

        var buffer = new byte[100];
        var streamLen = stream.Length;
        var currNumOfBytes = 0;

        await using (Stream fs = new FileStream(FileName, FileMode.Create))
        {
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                currNumOfBytes += read;

                await Task.Delay(1);
                await fs.WriteAsync(buffer, 0, read);

                prog.Report((int)(currNumOfBytes * 100 / streamLen));
            }
        }

        Console.WriteLine($"\nКопирование завершено в поток: {Thread.CurrentThread.ManagedThreadId}\n");

        _sem.Release();
    }

    public async Task<int> GetStatisticsAsync(Func<T,bool> filter)
    {
        Console.WriteLine($"GetStatisticsAsync начал выполняться в потоке: {Thread.CurrentThread.ManagedThreadId}");

        using (FileStream fs = new FileStream(FileName, FileMode.Open))
        {
            var col = await JsonSerializer.DeserializeAsync<T[]>(fs);

            if (col == null)            
                throw new Exception("Некорректный файл");
           
            return col.Count(filter);
        }

    }
}
