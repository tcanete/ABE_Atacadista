using System;
using System.Collections.Generic;

namespace ABE_Atacadista.Models
{
    public class OrderRequestDTO : BaseDTO
    {
        public int Id { get; set; } 
        public decimal Value { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderStatus Status { get; set; }
        public List<Order> Orders { get; set; }
    }
}