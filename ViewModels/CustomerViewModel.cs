using EComerceApi.Models;

namespace EComerceApi.ViewModels;

public class CustomerViewModel
{
    public string Name { get; set; }
      
    public Guid UserId { get; set; } 
  
    public List<Order> Orders { get; set; } = new List<Order>();
}