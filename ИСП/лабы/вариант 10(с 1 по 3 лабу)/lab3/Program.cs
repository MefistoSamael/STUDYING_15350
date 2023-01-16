using _153501_Bychko_Lab3.Entities;
using _153501_Bychko_Lab3.Journal;

Shop internetShop = new Shop();
Journal journal = new Journal();

internetShop.AddItemInStock("TV", 100);
internetShop.AddItemInStock("KeyBoard", 30);
internetShop.AddItemInStock("Mouse", 30);
internetShop.AddItemInStock("PC", 120);

internetShop.ProdInStockSorted();

internetShop.AddUser("Ivanov");
internetShop.AddUser("Petrov");
internetShop.AddUser("Sidorov");

internetShop.LogIn("Ivan");
internetShop.LogIn("Ivanov");

internetShop.AddItemInBasket("PC");
internetShop.AddItemInBasket("KeyBoard");
internetShop.AddItemInBasket("Mouse");

internetShop.BuyItems();
internetShop.TotalPriceClient();
internetShop.LogIn("Petrov");

internetShop.AddItemInBasket("PC");
internetShop.AddItemInBasket("KeyBoard");
internetShop.AddItemInBasket("Mouse");
internetShop.AddItemInBasket("TV");

internetShop.BuyItems();
internetShop.TotalPriceClient();

internetShop.LogIn("Sidorov");

internetShop.AddItemInBasket("PC");
internetShop.AddItemInBasket("KeyBoard");
internetShop.AddItemInBasket("Mouse");
internetShop.AddItemInBasket("TV");

internetShop.BuyItems();

internetShop.AddItemInBasket("PC");
internetShop.AddItemInBasket("PC");
internetShop.AddItemInBasket("PC");

internetShop.BuyItems();

internetShop.TotalPriceClient();

internetShop.TotalBaughtProductPrice();

internetShop.MaxSumClient();

internetShop.ClientsAboveSum(200);

internetShop.SumByProduct();
