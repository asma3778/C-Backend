using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static sda_3_online_Backend_Teamwork.src.DTO.ProductDTO;

namespace sda_3_online_Backend_Teamwork.src.DTO
{
    public class OrderItemDTO
    {
        public class OrderItemCreateDto
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public class OrderItemReadDto
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
            public ProductReadDto product { get; set; }
        }
    }
}
