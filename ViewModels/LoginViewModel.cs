using System.ComponentModel.DataAnnotations;

namespace EComerceApi.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "o email e requerido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha e requerida")]
    public string Password { get; set; }
}