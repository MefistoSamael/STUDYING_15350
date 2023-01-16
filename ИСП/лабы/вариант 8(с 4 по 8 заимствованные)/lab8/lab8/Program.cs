using LibraryLab8;

{
    StreamService<Employee> service = new StreamService<Employee>();
    MemoryStream mStream = new MemoryStream();
    Progress progress = new Progress();
    progress.progress += (int progress) 
        => Console.Write($"\rПоток: {Thread.CurrentThread.ManagedThreadId}: {progress}%");

    Employee[] employees = new Employee[1000];

    for(int i = 0; i < employees.Length; i++)
    {
        employees[i] = new Employee(i, i % 74, "Ivanov" + i);
    }

    Task task1 = service.WriteToStreamAsync(mStream, employees, progress);

    Task task2 = service.CopyFromStreamAsync(mStream, "Employee.json", progress);

    //result позволяет ждет завершения метода и возвращает значение
    //в нашем случае инт
    int answ = service.GetStatisticsAsync("Employee.json", x => x.Age > 35).Result;
    Console.WriteLine("Работников старше 35: " + answ);
}
