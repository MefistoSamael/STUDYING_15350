using _153501_Bychko_Lab5.Entities;

Shop InternetShop = new Shop();

InternetShop.ProductsInStock.Add(new Product(100, "TV"));
InternetShop.ProductsInStock.Add(new Product(200, "PC"));
InternetShop.ProductsInStock.Add(new Product(20, "KeyBoard"));
InternetShop.ProductsInStock.Add(new Product(10, "Mouse"));

InternetShop.Users.Add(new User("Ivanov"));
InternetShop.Users.Add(new User("Petrov"));
InternetShop.Users.Add(new User("Sidorov"));


InternetShop.Users[0].basket.Add(InternetShop.ProductsInStock[0]);
InternetShop.Users[0].basket.Add(InternetShop.ProductsInStock[2]);

InternetShop.Users[1].basket.Add(InternetShop.ProductsInStock[1]);
InternetShop.Users[1].basket.Add(InternetShop.ProductsInStock[3]);

InternetShop.Users[2].basket.Add(InternetShop.ProductsInStock[0]);
InternetShop.Users[2].basket.Add(InternetShop.ProductsInStock[1]);
InternetShop.Users[2].basket.Add(InternetShop.ProductsInStock[2]);
InternetShop.Users[2].basket.Add(InternetShop.ProductsInStock[3]);


InternetShop.Users[0].WriteBasket();
Console.WriteLine("");
InternetShop.Users[1].WriteBasket();
Console.WriteLine("");
InternetShop.Users[2].WriteBasket();