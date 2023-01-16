using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _153501_Bychko_Lab2.Journal
{
    internal class Journal
    {
        IEnumerable<string>? productsList;
        IEnumerable<string>? userList;

        public void UserChanged(IEnumerable<string> user)
        {
            Console.WriteLine("список пользователей изменился");

            userList = user;
        }

        public void ProductStuck(IEnumerable<string> products)
        {
            Console.WriteLine("список товаров изменился");

            productsList = products;
        }

        public void UsersChangeList()
        {
            Console.WriteLine("Новый список пользователей:");
            foreach (var item in userList)
                Console.WriteLine(item + "\n");
        }

        public void ProductChangeList()
        {
            Console.WriteLine("Новый список товаров: ");
            foreach (var item in productsList)
                Console.WriteLine(item + "\n");
        }
    }
}
