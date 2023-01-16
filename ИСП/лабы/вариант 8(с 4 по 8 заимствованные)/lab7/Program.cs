using lab7;

{
    Sinus.End += (int id, double answer, double time) =>
    {
        Console.SetCursorPosition(0, id);
        Console.WriteLine("Поток: " + id + ": Завершен с результатом: " + answer + " за время " + time + "\n");
    };
    
    Sinus.Progress += (int id, int progress) =>
    {
        string prog = new('=', progress / 10);
        prog += '>';
        prog += new string(' ',10 - progress / 10);

        Console.SetCursorPosition(1, id%2 +1);
        Console.Write("                                                                         ");
        Console.SetCursorPosition(1, id % 2 +1);
        Console.Write("Поток " + id + ": [" + prog +"] " + progress + "%");


    };

    Console.SetCursorPosition(0, 0);
    Thread thread1 = new Thread(Sinus.Rectangle) { Priority = ThreadPriority.Highest };
    Thread thread2 = new Thread(Sinus.Rectangle) { Priority = ThreadPriority.Lowest };
    thread1.Start();
    Thread.Sleep(100);
    thread2.Start();

    for(int i = 0; i < 5; i++)
    {
        Thread u = new Thread(Sinus.Rectangle) { Priority = ThreadPriority.Normal };
        u.Start();
    }
}