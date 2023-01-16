using _153501_Bychko_Lab3.Collections;


namespace _153501_Bychko_Lab3.Entities
{
    public record Shop
    {
        private Dictionary<string,Product> productsInStock;
        private MyCustomCollection<User> users;

        private User? currentUser;

        public event Action<IEnumerable<string>>? AddedUser;
        public event Action<IEnumerable<string>>? AddedProduct;
        public event Action<IEnumerable<string>, double>? BoughtProduct;


        public Shop()
        {
            productsInStock = new Dictionary<string, Product>();
            users = new MyCustomCollection<User>();
            currentUser = null;
        }

        public bool LogIn(string userName)
        {
            //ищем пользователя. Если есть меняем нынешнего

            var user = users.Find(x => x.Name == userName);
            if (user != null)
                currentUser = user.Value;
            else
            {
                Console.WriteLine($"пользователя {userName} не существует");
                Console.WriteLine();
                return false;
            }

            Console.WriteLine("Привет, " + userName);
            Console.WriteLine();
            return true;
        }

        public void LogOut()
        {
            if (currentUser != null)
                Console.WriteLine($"Пока {currentUser.Name}. Возвращайся поскорей");
            currentUser = null;
            Console.WriteLine();
        }

        public bool AddItemInBasket(string productName)
        {
            //если есть текущий пользователь
            if(currentUser != null)
            {
                //если есть товар на складе
                //добавляем в корзину
                var product = productsInStock[productName];
                if (product != null)
                    currentUser!.AddItemInBasket(product);
                else
                {
                    Console.WriteLine($"Товар {productName} остутствует на складе");
                    Console.WriteLine();
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Войдите в аккаунт");
                Console.WriteLine();
                return false;
            }

            return true;
        }

        public void AddItemInStock(string productName, double price)
        {
            productsInStock.Add(productName, new Product(price, productName));

            AddedProduct?.Invoke(productsInStock.Select(p => p.Value.Name));
        }

        public void AddUser(string name)
        {
            users.Add(new User(name));

            AddedUser?.Invoke(users.Select(x => x.Name));
        }

        public void BuyItems()
        {
            IEnumerable<string> currentUserBasket = currentUser?.ReturnBasket();
            var basketPrice = currentUser.BasketPrice();

            currentUser.BuyProducts();

            //вызов события с проверкой а на null
            BoughtProduct?.Invoke(currentUserBasket, basketPrice);

        }

        public void ProdInStockSorted()
        {
            var SortedList = from p in productsInStock
                             orderby p.Value.Price
                             select p;

            Console.WriteLine("товары:");
            foreach (var product in SortedList)
                Console.WriteLine($"{product.Value.Name} {product.Value.Price}");

            Console.WriteLine();
        }

        public void TotalBaughtProductPrice()
        {
            double price = 0;
            foreach (var user in users)
                price += user.GetSpentMoney();

            Console.WriteLine($"Стоимость всех купленных товаров в магазине: {price}");
            Console.WriteLine();
        }

        public void TotalPriceClient()
        {
            if(currentUser == null) return;
            Console.Write($"общая стоимость всех товаров, заказанных клиентом {currentUser.Name} в соответствии с действующими тарифами: ") ;
            Console.WriteLine(currentUser.GetSpentMoney());
            Console.WriteLine();
        }

        public void MaxSumClient()
        {
            var client = users.Aggregate((max, next) => max.GetSpentMoney() > next.GetSpentMoney()
                                                                         ? max : next);

            Console.WriteLine($"Клиент потртивший больше всего денег: {client.Name}");
            Console.WriteLine();
        }

        public void ClientsAboveSum(double sum)
        {
            Console.WriteLine($"Количество пользователей потративших денег больше чем {sum} : {users.Aggregate(0, (x, y) => y.GetSpentMoney() > sum ? x + 1 : x)}"); 
        }

        public void SumByProduct()
        {
            var list =  currentUser.GetBoughtProducts().GroupBy(x => x.Name).Select(g => (g.Key, g.Sum(p => p.Price)));
            foreach (var product in list)
                Console.WriteLine($"За продукт {product.Key} пользователь {currentUser.Name} заплатил {product.Item2}");

            Console.WriteLine();
        }
    }
}
