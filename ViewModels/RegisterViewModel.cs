using System.ComponentModel.DataAnnotations;
using EComerceApi.Models;

namespace EComerceApi.ViewModels;

public class RegisterViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "o nome e requerido")]
    public string Name { get; set; }
    [Required(ErrorMessage = "o email e requerido")]
    [EmailAddress(ErrorMessage = "email invalido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O slug e requerido")]

    public string Slug { get; set; }
    
    [Required(ErrorMessage = "o papel (role) é requerido")]
    public CustumerViewModel Customer { get; set; }
    
    
}