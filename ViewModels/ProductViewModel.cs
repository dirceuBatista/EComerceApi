using System.ComponentModel.DataAnnotations;

namespace EComerceApi.ViewModels;

public class ProductViewModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}