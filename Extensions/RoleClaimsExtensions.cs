using System.Security.Claims;
using EComerceApi.Models;

namespace EComerceApi.Extensions;

public static class RoleClaimsExtensions
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email)
        };
        result.AddRange(
            user
                .Roles
                .Select<Role, Claim>(role =>new Claim(
                    ClaimTypes.Role, role.Slug)));
        return result;
    }
}