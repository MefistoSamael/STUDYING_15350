using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClassLibrary;
using LoremNET;

namespace laba8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string path = @"D:\УНИК\прога\исп\laba8\laba8.json";
            StreamService<Shooter> streamService = new();
            MemoryStream memoryStream = new();

            Progress progress = new();
            progress.progress += (int progress)
                => Console.Write($"\rПоток {Thread.CurrentThread.ManagedThreadId}: {progress}%");

            Shooter[] employees = new Shooter[1000];
            Random rnd = new();
            for (int i = 0; i < employees.Length; i++)
                employees[i] = new Shooter((i + 1), LoremNET.Lorem.Words(1), rnd.Next(1,100));

            Task task1 = streamService.WriteToStreamAsync(memoryStream, employees, progress);
            Task task2 = streamService.CopyFromStreamAsync(memoryStream, path, progress);

            int result = await streamService.GetStatisticsAsync(path, x => x.Score > 80);
            Console.WriteLine("Количество стрелков, получивших более 80 баллов: " + result);
        }
    }
}
