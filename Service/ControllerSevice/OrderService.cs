using AutoMapper;
using EComerceApi.Data;
using EComerceApi.InputModel;
using EComerceApi.Models;
using EComerceApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EComerceApi.Service.ControllerSevice;

public class OrderService(AppDbContext context,IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<ResultViewModel<OrderViewModel>> CreateOrder(
        OrderInputModel model)
    {
        var existingOrder = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        if (existingOrder != null)
        {
            return new ResultViewModel<OrderViewModel>("Pedido já existe.");
        }
        var order = new Order
        {
            CustomerId = model.CustomerId,
            OrderItems = new List<OrderItem>()
        };
        order.OrderItems = model.OrderItems.Select(item => new OrderItem
        {
            
            OrderId = order.Id,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            Total = item.Quantity * item.UnitPrice
        }).ToList();
        
        try
        {
            await _context
                .Orders
                .AddAsync(order);
            await _context.SaveChangesAsync();
            var orderDto = _mapper.Map<OrderViewModel>(order);
            return new ResultViewModel<OrderViewModel>(orderDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<OrderViewModel>(
                $"Erro Interno{e.Message}");
        }
    }
    public async Task<ResultViewModel<List<OrderViewModel>>> GetOrder()
    {
        try
        {
            var orders = await _context
                .Orders
                .Include(x=>x.OrderItems)
                .ToListAsync();
            var ordersDto = _mapper.Map<List<OrderViewModel>>(orders);
            return new ResultViewModel<List<OrderViewModel>>(ordersDto);
        }
        catch (Exception e)
        {
            
            return new ResultViewModel<List<OrderViewModel>>(
                $"Erro Interno{e.Message}");
        }
    }

    public async Task<ResultViewModel<OrderViewModel>> GetOrderById(Guid id)
    {
        try
        {
            var order = await _context
                .Orders
                .Include(x=>x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return new ResultViewModel<OrderViewModel>(
                    "Pedido não encontrado");
            var orderDto = _mapper.Map<OrderViewModel>(order);
            return new ResultViewModel<OrderViewModel>(orderDto);
        }
        catch (Exception e)
        {

            return new ResultViewModel<OrderViewModel>(
                $"Erro Interno{e.Message}");
        }
    }
}