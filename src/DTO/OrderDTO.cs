using sda_3_online_Backend_Teamwork.src.Entity;
using static sda_3_online_Backend_Teamwork.src.DTO.OrderItemDTO;

namespace sda_3_online_Backend_Teamwork.src.DTO
{
    public class OrderDTO
    {
        public class OrderCreateDto
        {
            //public Guid OrderId { get; set; }
            public DateTime? OrderDate { get; set; } = DateTime.UtcNow;

            //  public Guid UserId { get; set; }
            public decimal TotalAmount { get; set; }
            public string? Status { get; set; }
            public required int PaymentMethodId { get; set; }
            public ICollection<OrderItemCreateDto> OrderItems { get; set; } =
                new List<OrderItemCreateDto>();
        }

        public class OrderReadDto
        {
            public Guid OrderId { get; set; }

            public DateTime OrderDate { get; set; }
            public Guid UserId { get; set; }

            public decimal TotalAmount { get; set; }
            public string? Status { get; set; }
            public int PaymentMethodId { get; set; }
            public ICollection<OrderItemReadDto> OrderItems { get; set; } =
                new List<OrderItemReadDto>();
        }

        public class OrderUpdateDto
        {
            public string? Status { get; set; }
        }
    }
}
