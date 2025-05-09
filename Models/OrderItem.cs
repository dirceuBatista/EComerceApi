using Microsoft.AspNetCore.Authorization;

namespace EComerceApi.Models;

public class OrderItem 
{
    
    
        public Guid Id { get; set; }= Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        public decimal Total { get; set; }
    

}