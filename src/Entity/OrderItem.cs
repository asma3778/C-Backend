using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sda_3_online_Backend_Teamwork.src.Entity
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
