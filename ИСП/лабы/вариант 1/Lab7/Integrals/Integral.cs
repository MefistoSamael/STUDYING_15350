using System.Diagnostics;


namespace MyMaths;

public class Integral
{
    public static event Action<string, double, double>? EvaluatingEnd;
    public static event Action<string, int>? Progress;

    private static Semaphore _sem = new Semaphore(1, 6);

    public Integral()
    {
        _sem.Release();
    }

    public static void Evaluate()
    {
        _sem.WaitOne();

        double its = 100000000;
        double j = 0;

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        double ans = 0;

        for (double i = 0; i < 1; i += 0.00000001, j++)
        {
            if (j % (its / 100) == 0)
                Progress?.Invoke(Thread.CurrentThread.Name ?? $"ID:{ Thread.CurrentThread.ManagedThreadId }", (int)Math.Round(j / its * 100));

            ans += Sin(i) * 0.00000001;
        }

        Progress?.Invoke(Thread.CurrentThread.Name ?? $"ID:{ Thread.CurrentThread.ManagedThreadId }", 100);

        stopWatch.Stop();

        EvaluatingEnd?.Invoke(Thread.CurrentThread.Name ?? $"ID:{ Thread.CurrentThread.ManagedThreadId }", ans, stopWatch.Elapsed.TotalSeconds);

        _sem.Release();

        double Sin(double x)
        {
            double ans = x, prevIt = x;

            // На каждом шагe вместо вычисления i-того члена ряда заного, этот член 
            // вычисляется с учётом предыдущего
            for (int i = 1; prevIt != 0; i++)
                ans += prevIt *= x * x / (2 * i + 1) / (2 * i) * -1;

            return ans;
        }
    }

    
}