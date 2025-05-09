using System.ComponentModel.DataAnnotations;
using EComerceApi.Models;

namespace EComerceApi.ViewModels;

public class UserViewModel
{
    public Guid Id{ get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Slug { get; set; }
    public CustumerViewModel Customer { get; set; }
}