namespace _153501_Bychko_Lab5.Entities
{
    public record Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(double price, string name)
        {
            Price = price;
            Name = name;
        }
    }
}
