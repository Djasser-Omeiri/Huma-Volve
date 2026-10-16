namespace OmeiriDjasser.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
