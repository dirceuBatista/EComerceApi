using EComerceApi.Service.ControllerSevice;
using Microsoft.AspNetCore.Mvc;

namespace EComerceApi.Controller;
[ApiController]
public class ProductController(ProductService product) : ControllerBase
{
    [HttpGet("v1/products")]
    public async Task<IActionResult> GetProduct()
    {
        var result = await product.GetProduct();
        if (!result.Success)
            return Conflict(new { erros = result.Errors });
        return Ok(result);
        
    }
}