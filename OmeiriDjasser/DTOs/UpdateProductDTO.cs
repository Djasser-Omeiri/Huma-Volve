namespace OmeiriDjasser.DTOs
{
    public class UpdateProductDTO
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public byte[] RowVersion { get; set; } = [];
    }
}
