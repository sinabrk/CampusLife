using BG.CampusLife.Domain.Enums;
using System;
using System.Collections.Generic;

namespace BG.CampusLife.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public List<Product> Products { get; set; }
        public Guid BuyerId { get; set; }
        public User Buyer { get; set; }
        public DateTime Created { get; set; }
        public Status OrderStatus { get; set; }
    }
}
