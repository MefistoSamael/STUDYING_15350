using _153501_Bychko_Lab5.Entities;
using _153501_Bychko_Lab2.Journal;

string? boughtItems = null;

Shop internetShop = new Shop();

Journal journal= new Journal();

internetShop.regestrateAddedProduct(journal.ProductStuck);
internetShop.regestrateAddedUser(journal.UserChanged);
internetShop.regestrateBoughtProduct(str =>
{
    Console.WriteLine("Список купленных товаров изменен");

    Console.WriteLine("Список купленных товаров: ");
    if (boughtItems != null) Console.WriteLine(boughtItems);
    foreach (string str2 in str)
    {
        Console.WriteLine(str2 + "\n");
        boughtItems += str2 + "\n";
    }
});

internetShop.AddItemInStock("TV", 100);
internetShop.AddItemInStock("KeyBoard", 30);
internetShop.AddItemInStock("Mouse", 30);
internetShop.AddItemInStock("PC", 120);

internetShop.AddUser("Ivanov");
internetShop.AddUser("Petrov");
internetShop.AddUser("Sidorov");

internetShop.LogIn("Ivan");
internetShop.LogIn("Ivanov");

internetShop.AddItemInBasket("PC");
internetShop.AddItemInBasket("KeyBoard");
internetShop.AddItemInBasket("Mouse");

internetShop.BuyItems();

internetShop.LogOut();
