using AutoMapper;
using EComerceApi.Data;
using EComerceApi.Models;
using EComerceApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EComerceApi.Service.ControllerSevice;

public class ProductService(AppDbContext context, IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ResultViewModel<List<ProductViewModel>>> GetProduct()
    {
        try
        {
            var products = await _context.Products.ToListAsync();
            var productsDto = _mapper.Map<List<ProductViewModel>>(products);
            return new ResultViewModel<List<ProductViewModel>>(productsDto);
        }
        catch (Exception e)
        {

            return new ResultViewModel<List<ProductViewModel>>(
                $"Erro Interno{e.Message}");
        }
    }

    public async Task<ResultViewModel<ProductViewModel>> GetProductById(Guid id)
    {
        try
        {
            var product = await _context
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return new ResultViewModel<ProductViewModel>(
                    "Produto não encontrado");
            var productsDto = _mapper.Map<ProductViewModel>(product);
            return new ResultViewModel<ProductViewModel>(productsDto);
        }
        catch (Exception e)
        {

            return new ResultViewModel<ProductViewModel>(
                $"Erro Interno{e.Message}");
        }
    }
    public async Task<ResultViewModel<List<ProductViewModel>>> GetProductInStock(bool inStock)
    {
        try
        {
            var productsInStock = await _context.Products
                .Where(p => p.InStock == true)
                .ToListAsync();
            if (productsInStock == null || productsInStock.Count == 0)
                return new ResultViewModel<List<ProductViewModel>>(
                    "nao tem produto no estoque");
            var productsDto = _mapper.Map<List<ProductViewModel>>(productsInStock);
            return new ResultViewModel<List<ProductViewModel>>(productsDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<List<ProductViewModel>>(
                $"Erro Interno{e.Message}");
        }
        
    }

    public async Task<ResultViewModel<ProductViewModel>> CreateProduct(
        ProductViewModel model)
    {

        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(x => x.Name == model.Name);
        if (existingProduct != null)
            return new ResultViewModel<ProductViewModel>(
                "Já existe um produto com este nome");

        var product = new Product
        {
            Name = model.Name,
            Price = model.Price,
            InStock = true
            
        };
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var productsDto = _mapper.Map<ProductViewModel>(product);
            return new ResultViewModel<ProductViewModel>(productsDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<ProductViewModel>(
                $"Erro Interno{e.Message}");
        }
    }

    public async Task<ResultViewModel<ProductViewModel>> UpdateProduct(
        ProductViewModel model, Guid id)
    {
        var product = await _context
            .Products
            .FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
            return
                new ResultViewModel<ProductViewModel>(
                    "Produto não encontrado");
        product.Name = model.Name;
        product.Price = model.Price;
        product.InStock = model.InStock;

        try
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            var productsDto = _mapper.Map<ProductViewModel>(product);
            return new ResultViewModel<ProductViewModel>(productsDto);

        }
        catch (Exception e)
        {
            return new ResultViewModel<ProductViewModel>(
                $"Erro Interno{e.Message}");
        }

    }

    public async Task<ResultViewModel<ProductViewModel>> DeleteProduct(Guid id)
    {
        var product = await _context
            .Products
            .FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
            return
                new ResultViewModel<ProductViewModel>(
                    "Erro ao processar requisição");
        try
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            var productsDto = _mapper.Map<ProductViewModel>(product);
            return new ResultViewModel<ProductViewModel>(productsDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<ProductViewModel>(
                $"Erro Interno{e.Message}");
        }
    }
}


 