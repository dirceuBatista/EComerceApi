using AutoMapper;
using EComerceApi.Data;
using EComerceApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EComerceApi.Service.ControllerSevice;

public class ProductService(AppDbContext context, IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ResultViewModel<List<ProductViewModel>>> GetProduct()
    {
       var products = await _context.Products.ToListAsync();
       var productsDto = _mapper.Map<List<ProductViewModel>>(products);
       return new ResultViewModel<List<ProductViewModel>>(productsDto);
    }
}