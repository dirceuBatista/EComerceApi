using EComerceApi.Models;

namespace EComerceApi.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }
    
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    
}