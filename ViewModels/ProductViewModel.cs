using System.ComponentModel.DataAnnotations;

namespace EComerceApi.ViewModels;

public class ProductViewModel
{
    [Required(ErrorMessage = "O Nome do produto e requerido")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O preço do produto e requerido")]
    public string Price { get; set; }
}