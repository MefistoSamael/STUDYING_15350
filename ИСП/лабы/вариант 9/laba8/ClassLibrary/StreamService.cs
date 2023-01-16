using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class StreamService<A>
    {
        Semaphore sem = new Semaphore(1, 3);
        public async Task WriteToStreamAsync(Stream stream, IEnumerable<A> data, Progress progress) // записывает коллекцию data в поток stream
        {
            sem.WaitOne();
            Console.WriteLine("Начало записи. Id потока: " + Thread.CurrentThread.ManagedThreadId);
            for (int i = 1; i <= 100; i++)
            {
                if (i == 50)
                    await JsonSerializer.SerializeAsync(stream, data);
                await Task.Delay(40); // задержка асинхронной операции
                progress.Report(i);
            }
            stream.Position = 0;
            Console.WriteLine("\nОкончание записи. Id потока: " + Thread.CurrentThread.ManagedThreadId);
            sem.Release();
        }

        public async Task CopyFromStreamAsync(Stream stream, string fileName, Progress progress) // копирует информацию из потока stream в файл с именем fileName 
        {
            sem.WaitOne();
            Console.WriteLine("\nНачало копирования. Id потока: " + Thread.CurrentThread.ManagedThreadId);
            await using (Stream fileStream = new FileStream(fileName, FileMode.Create))
            {
                for (int i = 1; i <= 100; i++)
                {
                    if (i == 50)
                        await stream.CopyToAsync(fileStream);
                    await Task.Delay(20);
                    progress.Report(i);
                }
            }
            stream.Position = 0;
            Console.WriteLine("\nОкончание копирования. Id потока: " + Thread.CurrentThread.ManagedThreadId);
            sem.Release();
        }

        public async Task<int> GetStatisticsAsync(string fileName, Func<A,bool> filter) // считывает объекты типа A из файла с именем fileName и возвращает
                                                                                        // количество объектов, удовлетворяющих условию filter
        {
            sem.WaitOne();
            int count = 0;
            await using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                var shooters = await JsonSerializer.DeserializeAsync<A[]>(fileStream);
                if (shooters != null)
                    foreach (var element in shooters)
                        if (filter(element))
                            count++;
            }
            sem.Release();
            return count;
        }
    }
}
