using System.Security.Claims;

namespace EComerceApi.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    
    public string Slug { get; set; }
    public string PasswordHash { get; set; }
    
    public List<Role> Roles { get; set; }
    public Customer Customer { get; set; }

    
}