using EComerceApi.Data;
using EComerceApi.Models;
using EComerceApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace EComerceApi.Service.ControllerSevice;

public class AccountService(AppDbContext context, TokenService.TokenService tokenService)
{
    private readonly AppDbContext _context = context;
    public async Task<ResultViewModel<dynamic>> CreateAccount(
        RegisterViewModel model)
    {
        var user = new User
        {
            Name = model.Name,
            Email = model.Email.ToLower(),
            Slug = model.Slug,
            Customer = new Customer()
            {
                UserId = model.Id,
                Name = model.Name
            }
            
        };
        var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password);
        try
        {
         
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new ResultViewModel<dynamic>(
                new { user = user.Email, password });
        }
        catch (Exception e)
        {
            return new ResultViewModel<dynamic>(e.InnerException?.Message);
        }
    }

    public async Task<ResultViewModel<UserViewModel>> Login(
        LoginViewModel login)
    {
        var email = login.Email?.Trim().ToLower();
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null || !PasswordHasher.Verify(user.PasswordHash, login.Password))
            return new ResultViewModel<UserViewModel>("Usuario invalido");
        try
        {
            var token = tokenService.GenerateToken(user);
            return new ResultViewModel<UserViewModel>(token);
        }
        catch (Exception e)
        {
           return new ResultViewModel<UserViewModel>( e.Message);
            
        }
    }
}