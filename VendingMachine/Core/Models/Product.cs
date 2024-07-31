namespace VendingMachine.Core.Models
{
    public class Product : BaseEntity
    {
        public int AmountAvailable { get; set; }
        public double Cost { get; set; }
        public string ProductName { get; set; } = null!;
        public string SellerId { get; set; } = null!;
        public User? Seller { get; set; }
    }
}
