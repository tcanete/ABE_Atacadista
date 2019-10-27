using System.Collections.Generic;

namespace ABE_Atacadista.Models
{
    public class OrderRequestDTO
    {
        public int Id { get; set; } 
        public OrderStatus Status { get; set; }
        public List<Order> Orders { get; set; }
    }
}