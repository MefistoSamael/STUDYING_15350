using _153501_Bychko_Lab3.Collections;

namespace _153501_Bychko_Lab3.Entities
{
    public record User
    {
        public string Name { get;private  set; }
        private List<Product> basket;
        private List<Product> boughtProducts;


        public User(string name)
        {
            Name = name;
            basket = new List<Product>();
            boughtProducts = new List<Product>();
        }

        public IEnumerable<string> ReturnBasket() => basket.Select(p => p.Name);

        public double BasketPrice()
        {
            double price = 0;
            foreach (Product p in basket)
            {
                price += p.Price;
            }

            return price;
        }

        public void BuyProducts()
        {
            boughtProducts.AddRange(basket);
            basket.Clear();
        }

        public double GetSpentMoney() => boughtProducts.Sum(p => p.Price);

        public List<Product> GetBoughtProducts() => boughtProducts;

        public void AddItemInBasket(Product product) => basket.Add(product);
    }
}
