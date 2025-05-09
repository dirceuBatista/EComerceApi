using EComerceApi.Extensions;
using EComerceApi.InputModel;
using EComerceApi.Service.ControllerSevice;
using EComerceApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EComerceApi.Controller;
[ApiController]
public class OrderController(OrderService orderService) : ControllerBase
{
    /// <summary>
    /// Retorna todos os pedidos.
    /// </summary>
    /// <remarks>Este endpoint retorna todos os pedidos cadastrados no sistema.</remarks>
    /// <returns>Lista de pedidos</returns>
    /// <response code="200">Pedidos retornados com sucesso.</response>
    /// <response code="500">Erro interno ao processar a solicitação.</response>
    [HttpGet("v1/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrders()
    {
        var result = await orderService.GetOrder();
        if (!result.Success)
            return StatusCode(500,new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Retorna um pedido específico pelo ID.
    /// </summary>
    /// <param name="id">O ID do pedido que será retornado.</param>
    /// <returns>Detalhes do pedido ou erro caso não encontrado.</returns>
    /// <response code="200">Pedido retornado com sucesso.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno ao processar a solicitação.</response>
    [HttpGet("v1/orders/{id:guid}")]
    [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]  
    [ProducesResponseType(StatusCodes.Status404NotFound)]  
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]  
   
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var result = await orderService.GetOrderById(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result.Data);
    }
    /// <summary>
    /// Cria um novo pedido.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite criar um novo pedido no sistema.
    /// O corpo da requisição deve conter as informações do pedido a ser criado.
    /// </remarks>
    /// <param name="model">O modelo contendo os dados do pedido a ser criado.</param>
    /// <returns>Retorna os detalhes do pedido ou um erro se houver falha na criação.</returns>
    /// <response code="200">Pedido criado com sucesso.</response>
    /// <response code="400">Se o modelo do pedido for inválido.</response>
    /// <response code="409">Se houver conflito ao criar o pedido (por exemplo, dados já existentes).</response>
    [HttpPost("v1/orders")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]  
    [ProducesResponseType(typeof(ResultViewModel<string>), StatusCodes.Status400BadRequest)]  
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync(
        [FromBody] OrderInputModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create =
            await orderService.CreateOrder(model);
        
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Ok($"{create.Data.OrderItems}");
    }
}