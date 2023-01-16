using System.Text;
using System.Text.Json;

namespace LibraryLab8
{
    public class StreamService<T>
    {
        //Mutex mutex = new Mutex();
        Semaphore sem = new Semaphore(1, 1);
        public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data, Progress progress)
        {
            sem.WaitOne();
            Console.WriteLine("Начало записи. Id потока: " + Thread.CurrentThread.ManagedThreadId);

            for(int i = 1; i < 101; i++)
            {
                //заносит данные в поток
                if(i == 50)
                    await JsonSerializer.SerializeAsync(stream, data);
                await Task.Delay(40);
                progress.Report(i);
            }

            //перевод картеки в начало потока
            stream.Position = 0;
            Console.WriteLine("\nОкончание записи. Id потока: " + Thread.CurrentThread.ManagedThreadId);
            sem.Release();
        }

        public async Task CopyFromStreamAsync(Stream stream, string filename, Progress progress)
        {
            sem.WaitOne();
            Console.WriteLine("\nНачало копирования. Id потока: " + Thread.CurrentThread.ManagedThreadId);

            
            await using (Stream fs = new FileStream(filename, FileMode.Create))
            {
                for (int i = 1; i < 101; i++)
                {
                    //заносим данные в поток
                    if (i == 50)
                        await stream.CopyToAsync(fs);
                    
                    await Task.Delay(20);
                    progress.Report(i);
                }
            }

            
            stream.Position = 0;
            Console.WriteLine("\nОкончание копирования. Id потока: " + Thread.CurrentThread.ManagedThreadId);
            sem.Release();
        }

        public async Task<int> GetStatisticsAsync(string fileName, Func<T, bool> filter)
        {
            sem.WaitOne();
            int count = 0;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                var employes = await JsonSerializer.DeserializeAsync<T[]>(fs);

                if (employes != null)
                {
                    foreach (var employee in employes)
                        if (filter(employee))
                            count++;
                }
            }
            sem.Release();
            return count;
        }
    }
}
