using static sda_3_online_Backend_Teamwork.src.DTO.OrderItemDTO;

namespace sda_3_online_Backend_Teamwork.src.Entity
{
    public class Order
    {
        public Guid OrderId { get; set; } // = Guid.NewGuid(); // Order_Id (PK)
        public DateTime OrderDate { get; set; } // Order_Date
        public decimal TotalAmount { get; set; } // TotalAmount
        public string Status { get; set; } // Status
        public int PaymentMethodId { get; set; } // PaymentMethodID (FK)

        public Guid UserId { get; set; } // User_Id (FK)

        // public User User { get; set; } // Navigation property

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
