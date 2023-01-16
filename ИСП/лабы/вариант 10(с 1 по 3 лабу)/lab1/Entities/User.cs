using _153501_Bychko_Lab5.Collections;

namespace _153501_Bychko_Lab5.Entities
{
    public record User
    {
        public string Name { get; private set; }
        public MyCustomCollection<Product> basket;

        public User(string name)
        {
            Name = name;
            basket = new MyCustomCollection<Product>();
        }

        public void WriteBasket()
        {
            basket.Reset();
            Console.WriteLine($"{Name} заказал товары:");
            for (; basket.current != null; basket.Next())
            {
                Console.WriteLine($"Название {basket.Current().Name}\n" +
                    $"цена - {basket.Current().Price}");
            }

            BasketPrice();
        }

        private void BasketPrice()
        {
            basket.Reset();
            double price = 0;
            for (; basket.current != null; basket.Next())
                price += basket.Current().Price;
            Console.WriteLine($"Общая сумма корзины {price}");
        }
    }
}
