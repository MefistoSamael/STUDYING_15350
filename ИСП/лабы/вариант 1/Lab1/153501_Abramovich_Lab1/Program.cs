using Collections;
using Entities;
using Interfaces;

class Program
{
    enum Command
    {
        UndefinedCommand,
        AddTarif,
        AddClient,
        RegisterCall,
        PersonTotalPrice,
        AllTotalPrice,     
        PrintTarifs,
        PrintAbonents,
        Finish 
    }

    static void Main(string[] args)
    {
        ATS ats = new ATS();

        PrintInstruction();

        while (true)
        {
            Console.Write("Введите комманду: ");
        
            Command command = GetCommand();

            switch (command)
            {
                case Command.AddTarif:
                    AddTarif();
                    break;
                case Command.AddClient:
                    AddAbonent();
                    break; 
                case Command.RegisterCall:
                    RegisterCall();
                    break;
                case Command.PersonTotalPrice:
                    ShowPersonPrice();
                    break;
                case Command.AllTotalPrice:
                    ShowAllPrice();
                    break;
                case Command.PrintTarifs:
                    PrintTarifs();
                    break;
                case Command.PrintAbonents:
                    PrintAbonents();
                    break;      
                case Command.Finish:
                    return;
                default:
                    Console.WriteLine("  Неизвестная комманда");
                    break;
            }
            Console.WriteLine();
        }

        void AddAbonent()
        {
            Console.Write("  Введите фамилию клиента : ");
            string surName = Console.ReadLine() ?? string.Empty;

            Console.Write("  Введите город клиента : ");
            string town = Console.ReadLine() ?? string.Empty;

            ats.AddAbonent(new Abonent(surName, town));
        }

        void AddTarif()
        {
            Console.Write("  Введите новый тариф : "); 
            int.TryParse(Console.ReadLine() ?? "0",out int tarif);

            if (tarif == 0)
            {
                Console.WriteLine("Некорректный ввод");
                return;
            }

            ats.AddTarif(new Tarif(tarif));
        }

        void RegisterCall()
        {
            Console.Write("  Введите город звонка : ");
            string town = Console.ReadLine() ?? string.Empty;

            Console.Write("  Введите время в минутах : ");
            int.TryParse(Console.ReadLine() ?? "0",out int time);

            Console.Write("  Введите номер клиента в списке : ");
            int abonentNum = int.Parse(Console.ReadLine() ?? "0");
            Abonent abonent = ats.GetAbonent(abonentNum);

            Console.Write("  Введите номер тарифа : ");
            int tarifNum = int.Parse(Console.ReadLine() ?? "0");
            Tarif tarif = ats.GetTarif(tarifNum);

            if (time == 0)
            {
                Console.WriteLine("Некорректный ввод");
                return;
            }

            ats.RegisterCall(new Call(town, time, abonent, tarif));
        }

        void ShowPersonPrice()
        {
            Console.Write("  Введите номер клиента в списке : ");
            int.TryParse(Console.ReadLine() ?? "0", out int num);

            int price = ats.GetAbonent(num)?.allPrice ?? -1;

            Console.WriteLine( (price == -1) 
                               ? "  Клиент не найден" 
                               : $"  Полная стоимость звонков клиента {ats.GetAbonent(num).surname} = {price}");
        }

        void ShowAllPrice()
        {
            int res = 0;

            foreach (Abonent abonent in ats.GetAbonentsSequence())
                res += abonent.allPrice;

            Console.WriteLine($"  Полная стоимость звонков клиентов = {res}");
        }

        void PrintAbonents()
        {
            foreach (Abonent abonent in ats.GetAbonentsSequence())
                Console.WriteLine(abonent);
        }

        void PrintTarifs()
        {
            foreach (Tarif tarif in ats.GetTarifsSequence())
                Console.WriteLine(tarif);
        }
    }

    static Command GetCommand()
    {
        int.TryParse(Console.ReadLine() ?? "0", out int c);
        return (Command)( ( c > 8 || c < 0) ? 0 : c);
    }

    static void PrintInstruction()
    {
        Console.WriteLine("  Добавить тариф - 1\n" +
                          "  Добавить абонента - 2\n" +
                          "  Зарегистрировать звонок - 3\n" +
                          "  Вывести стоимость звонков абонента - 4\n" +
                          "  Вывести стоимость звонков всех абонентов - 5\n" +
                          "  Вывести список тарифов - 6\n" +
                          "  Вывести список абонентов - 7\n" +
                          "  Закончить работу - 8\n");
    }
}
