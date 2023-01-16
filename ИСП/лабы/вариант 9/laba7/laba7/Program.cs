using System;
using System.Threading;
using ClassLibrary;

namespace laba7
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegralCalculation integralCalculation = new();
            integralCalculation.Calculating += integralCalculation.Calculate;

            Thread thread = new(integralCalculation.Func);
            thread.Name = "status = true";
            thread.Start();
            thread.Join();

            Thread thread1 = new(integralCalculation.Func);
            thread1.Priority = ThreadPriority.Lowest;
            Thread thread2 = new(integralCalculation.Func);
            thread2.Priority = ThreadPriority.Highest;
            thread1.Start();
            thread2.Start();

            Thread _thread1 = new(integralCalculation.Func);
            Thread _thread2 = new(integralCalculation.Func);
            Thread _thread3 = new(integralCalculation.Func);
            Thread _thread4 = new(integralCalculation.Func);
            Thread _thread5 = new(integralCalculation.Func);
            _thread1.Start();
            _thread2.Start();
            _thread3.Start();
            _thread4.Start();
            _thread5.Start();
        }
    }
}
