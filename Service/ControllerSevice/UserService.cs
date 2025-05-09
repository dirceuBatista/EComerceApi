using AutoMapper;
using EComerceApi.Data;
using EComerceApi.Models;
using EComerceApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EComerceApi.Service.ControllerSevice;

public class UserService(AppDbContext context, IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<ResultViewModel<List<UserViewModel>>> GetUsers()
    {
        try
        {
            var user = await _context
                .Users
                .Include(x=>x.Customer)
                .ToListAsync();
            var userDto = _mapper.Map<List<UserViewModel>>(user);
            return new ResultViewModel<List<UserViewModel>>(userDto);
        }
        catch (Exception e)
        {

            return new ResultViewModel<List<UserViewModel>>(
                $"Erro Interno{e.Message}");
        }
    }
    public async Task<ResultViewModel<UserViewModel>> GetUserById(Guid id)
    {
        try
        {
            var user = await _context
                .Users
                .Include(x=>x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return new ResultViewModel<UserViewModel>(
                    "Usuario não encontrado");
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);
        }
        catch (Exception e)
        {

            return new ResultViewModel<UserViewModel>(
                $"Erro Interno{e.Message}");
        }
    }
    public async Task<ResultViewModel<UserViewModel>> CreateUser(
        UserViewModel model)
    {

        var existingUser = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        if (existingUser != null)
            return new ResultViewModel<UserViewModel>(
                "Já existe um usuario com este email");

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Slug,
            Roles = new List<Role>(),
            Customer = new Customer()
            {
            UserId = model.Id,
            Name = model.Name
            }
        };
        
        try
        {
           
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            await _context.SaveChangesAsync();
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno{e.Message}");
        }
    }

    public async Task<ResultViewModel<UserViewModel>> UpdateUser(
        UserViewModel model, Guid id)
    {
        var user = await _context
            .Users
            .Include(x=>x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
            return
                new ResultViewModel<UserViewModel>(
                    "Usuario não encontrado");
        user.Name = model.Name;
        user.Email = model.Email;
        user.Slug = model.Slug;
        user.Customer.UserId = user.Id;
        user.Customer.Name = user.Name;
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);

        }
        catch (Exception e)
        {
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno{e.Message}");
        }

    }
    public async Task<ResultViewModel<UserViewModel>> DeleteUser(Guid id)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
            return
                new ResultViewModel<UserViewModel>(
                    "Erro ao processar requisição");
        try
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno{e.Message}");
        }
    }
}