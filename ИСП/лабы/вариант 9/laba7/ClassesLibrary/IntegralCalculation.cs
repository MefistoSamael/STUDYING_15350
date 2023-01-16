using System;
using System.Diagnostics;
using System.Threading;

namespace ClassLibrary
{
    public class IntegralCalculation
    {
        public double CalculateIntegral()
        {
            double h = 0.00000001;
            double a = 0;
            double b = 1;
            int n = (int) ((int) (b - a) / h);

            double percentage = 0;
            int path = n / 100;
            int k = 0;

            double result = 0;
            for (double x = a; x <= b; x += h)
            {
                result += Math.Sin(x) * h;

                int temp = 0;                       // задержка времени
                for (int i = 0; i < 222; i++)
                    temp = 2 * 2;

                k++;
                if (k % path == 0)
                {
                    percentage++;
                    string status = "[";
                    for (int i = 0; i < (percentage / 5 - 1); i++)
                        status += '=';
                    status += '>';
                    for (int i = status.Length - 1; i < 20; i++) 
                        status += ' ';
                    status += ']';

                    int id = Thread.CurrentThread.ManagedThreadId;
                    string text = $" Поток {id}: " + status + $" {percentage}%";
                    if (Thread.CurrentThread.Name != "status = true")
                        text += '\n';

                    Console.Write(text);
                    Console.SetCursorPosition(0, Console.CursorTop);                    
                }
            }

            return result;
        }

        static Semaphore sem = new Semaphore(2,2);
        public void Calculate()
        {
            sem.WaitOne();
             
            Stopwatch stopwatch = new();
            stopwatch.Start();
            double result = CalculateIntegral();
            stopwatch.Stop();
            double time = stopwatch.ElapsedMilliseconds / 1000;

            int id = Thread.CurrentThread.ManagedThreadId;

            Console.SetCursorPosition(0, id);
            Console.WriteLine($"\n Поток {id} завершён со следующим результатом:");
            Console.WriteLine($" - интеграл от y = sin(x) на промежутке [0; 1] = {result};");
            Console.WriteLine($" - всего затрачено на выполнение метода: {time} секунд(а).\n");

            sem.Release();
        }

        public delegate void CalculateHandler();
        public event CalculateHandler Calculating;
        public void Func() => Calculating?.Invoke();        
    }
}
