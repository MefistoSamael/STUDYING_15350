using _153501_Bychko_Lab5.Collections;


namespace _153501_Bychko_Lab5.Entities
{
    public record Shop
    {
        private MyCustomCollection<Product> productsInStock;
        private MyCustomCollection<User> users;

        private User? currentUser;

        public event Action<IEnumerable<string>>? AddedUser;
        public event Action<IEnumerable<string>>? AddedProduct;
        public event Action<IEnumerable<string>>? BoughtProduct;


        public Shop()
        {
            productsInStock = new MyCustomCollection<Product>();
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
                return false;
            }

            Console.WriteLine("Привет, " + userName);
            return true;
        }

        public void LogOut()
        {
            Console.WriteLine($"Пока {currentUser.Name}. Возвращайся поскорей");
            currentUser = null;
        }

        public bool AddItemInBasket(string productName)
        {
            //если есть текущий пользователь
            if(currentUser != null)
            {
                //если есть товар на складе
                //добавляем в корзину
                var product = productsInStock.Find(x => x.Name == productName);
                if (product != null)
                    currentUser!.AddItemInBasket(product.Value);
                else
                {
                    Console.WriteLine($"Товар {productName} остутствует на складе");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Войдите в аккаунт");
                return false;
            }

            return true;
        }

        public void AddItemInStock(string productName, double price)
        {
            productsInStock.Add(new Product(price, productName));

            AddedProduct?.Invoke(productsInStock.Select(p => p.Name));
        }

        public void AddUser(string name)
        {
            users.Add(new User(name));

            AddedUser?.Invoke(users.Select(x => x.Name));
        }

        public void BuyItems()
        {
            IEnumerable<string>? boughtItems = currentUser?.WriteBasket();

            //вызов события с проверкой а на null
            BoughtProduct?.Invoke(boughtItems ?? throw new Exception("Пользователь не выбран"));

        }

        public void regestrateAddedUser(Action<IEnumerable<string>> meth) => AddedUser += meth;

        public void regestrateAddedProduct(Action<IEnumerable<string>> meth) => AddedProduct += meth;

        public void regestrateBoughtProduct(Action<IEnumerable<string>> meth) => BoughtProduct += meth;
    }
}
