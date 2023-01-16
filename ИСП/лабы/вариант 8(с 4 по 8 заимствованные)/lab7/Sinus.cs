using System.Diagnostics;

namespace lab7
{
    internal class Sinus
    {
        public static event Action<int, double, double>? End;
        public static event Action<int, int>? Progress;

        //контроль за доступонстью кода для 
        static Semaphore sem = new Semaphore(2, 2);

        static public void Rectangle()
        {
            sem.WaitOne();

            double answ = 0;
            int it = 0;
            double u = 0;

            //считаем время выполнения программы
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (double xi = 0; xi <= 1.00000001; xi += 0.00000001, it++)
            {
                answ += Math.Sin(xi) * 0.00000001;
                if (it % 1000000 == 0)
                    Progress?.Invoke(Thread.CurrentThread.ManagedThreadId, it / 1000000);

                for (int i = 0; i < 10; i++)
                {
                    double k = 132 / 2467;
                    u *= k;
                }

            }

            stopWatch.Stop();

            End?.Invoke(Thread.CurrentThread.ManagedThreadId, answ, stopWatch.Elapsed.TotalSeconds);

            sem.Release();
        }
        
    }
}
