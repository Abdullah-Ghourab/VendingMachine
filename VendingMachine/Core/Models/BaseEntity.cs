namespace VendingMachine.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int CreatedById { get; set; }
        public User? CreatedBy { get; set; }
    }
}
