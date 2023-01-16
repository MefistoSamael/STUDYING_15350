using _153501_Bychko_Lab5.Collections;


namespace _153501_Bychko_Lab5.Entities
{
    public record Shop
    {
        public MyCustomCollection<Product> ProductsInStock;
        public MyCustomCollection<User> Users;

        public Shop()
        {
            ProductsInStock = new MyCustomCollection<Product>();
            Users = new MyCustomCollection<User>();
        }

    }
}
