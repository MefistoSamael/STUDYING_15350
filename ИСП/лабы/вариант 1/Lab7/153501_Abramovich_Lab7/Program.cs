using MyMaths;


class Program
{
    static void Main()
    {
        Integral.EvaluatingEnd += (string name,double answer, double time) => 
            Console.WriteLine($"Поток: {name} завершён. Результат = {answer}, время = {time} c");

        Integral.Progress += (string name, int progress) => 
            Console.WriteLine($"Поток: {name}\t[{new string('=',progress / 10)}>{new string(' ',10 - progress / 10)}]{progress}%");

        //Thread thread1 = new Thread(Integral.Evaluate) { Priority = ThreadPriority.Highest, Name = "1" };
        //Thread thread2 = new Thread(Integral.Evaluate) { Priority = ThreadPriority.Lowest, Name = "2" };

        //thread1.Start();
        //thread2.Start();

        Thread thread1 = new Thread(Integral.Evaluate) { Priority = ThreadPriority.Highest, Name = "1" };
        Thread thread2 = new Thread(Integral.Evaluate) { Priority = ThreadPriority.Lowest, Name = "2" };
        Thread thread3 = new Thread(Integral.Evaluate) { Priority = ThreadPriority.Lowest, Name = "3" };
        Thread thread4 = new Thread(Integral.Evaluate) { Priority = ThreadPriority.Lowest, Name = "4" };
        Thread thread5 = new Thread(Integral.Evaluate) { Priority = ThreadPriority.Lowest, Name = "5" };

        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();
        thread5.Start();
    }


}