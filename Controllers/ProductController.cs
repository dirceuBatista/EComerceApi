using EComerceApi.Extensions;
using EComerceApi.Service.ControllerSevice;
using EComerceApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EComerceApi.Controller;
[ApiController]
public class ProductController(ProductService product) : ControllerBase
{
    /// <summary>
    /// Reyornar todos os produtos.
    /// </summary>
    /// <returns>Lista de produtos</returns>
    [HttpGet("v1/products")]
    public async Task<IActionResult> GetProduct()
    {
        var result = await product.GetProduct();
        if (!result.Success)
            return StatusCode(500,new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Retorna os produtos que estão em estoque.
    /// </summary>
    /// <returns>Lista de produtos em estoque</returns>
    /// <response code="200">Produtos em estoque retornados com sucesso</response>
    /// <response code="400">Falha na requisição</response>
    [HttpGet("v1/products/inStock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductsInStock()
    {
        var result = await product.GetProductInStock(true);
        if (!result.Success)
            return BadRequest(new { errors = result.Errors });

        return Ok(result.Data);
    }

    /// <summary>
    /// Retornar um produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retornar um produto Buscado pelo id</returns>
    /// /// <response code="200">Produto encontrado com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    [HttpGet("v1/products/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var result = await product.GetProductById(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result.Data);
    }
    /// <summary>
    /// Cria um novo produto
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Produto criado</returns>
    /// /// <response code="201">Produto criado com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="409">Conflito (produto já existente)</response>
    [HttpPost("v1/products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PostAsync(
        [FromBody] ProductViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create =
            await product.CreateProduct(model);
        
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Ok(create.Data );
    }
    /// <summary>
    /// Atualizar um produto
    /// </summary>
    /// <param name="Informações atualizadas do produto"></param>
    /// <param name="id"></param>
    /// <returns>Produto atualizado</returns>
    /// /// <response code="200">Produto atualizado com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    /// <response code="400">Requisição inválida</response>
    [HttpPut("v1/products/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(
        [FromBody] ProductViewModel model, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var result = 
            await product.UpdateProduct(model,id);
        if (!result.Success)
            return Conflict(new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Deletar um produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns>produto deletado</returns>
    ///  <response code="200">Produto deletado com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    [HttpDelete("v1/products/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await product.DeleteProduct(id);
        if (!result.Success)
            return Conflict( new { erros = result.Errors });
        return Ok($"{result.Data.Name} - Produto deletado");
    }



    
}