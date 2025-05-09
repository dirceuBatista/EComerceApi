using EComerceApi.Extensions;
using EComerceApi.Service.ControllerSevice;
using EComerceApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EComerceApi.Controller;

[ApiController]
public class UserController(UserService userService) : ControllerBase
{
    /// <summary>
    /// retorna todos usuarios
    /// </summary>
    /// <returns>Lista de usuarios</returns>
    [HttpGet("v1/users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await userService.GetUsers();
        if (!result.Success)
            return StatusCode(500, new { erros = result.Errors });
        return Ok(result);
    }
    
    /// <summary>
    /// Retorna os dados de um usuário com base no ID fornecido.
    /// </summary>
    /// <param name="id do usuario no formato GUID"></param>
    /// <returns>Dados do usuário correspondente</returns>
    /// <response code="200">Usuário encontrado com sucesso.</response>
    /// <response code="400">ID inválido ou usuário não encontrado.</response>
    [HttpGet("v1/users/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdUser([FromRoute] Guid id)
    {
        var result = await userService.GetUserById(id);
        if (!result.Success)
            return BadRequest(new { erros = result.Errors });
        return Ok(result.Data);
    }
    
    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="usuário"></param>
    /// <returns>Produto criado</returns>
    /// <response code="201">Produto criado com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="409">Conflito (produto já existente)</response>
    [HttpPost("v1/users")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser(
        [FromBody] UserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create 
            = await userService.CreateUser(model);
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Ok(create);
    }
    
    /// <summary>
    /// Atualizar um usuário
    /// </summary>
    /// <param name="Informações atualizadas do usuário"></param>
    /// <param name="id"></param>
    /// <returns>Produto atualizado</returns>
    /// /// <response code="200">Produto atualizado com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    /// <response code="400">Requisição inválida</response>
    [HttpPut("v1/users/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(
        [FromBody]UserViewModel model,
        [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var result = await userService.UpdateUser(model, id);
        if (!result.Success)
            return Conflict(new { erros = result.Errors });
        return Ok(result);
    }
    
    /// <summary>
    /// Deletar um usuario
    /// </summary>
    /// <param name="id"></param>
    /// <returns>produto deletado</returns>
    ///  <response code="200">Produto deletado com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    [HttpDelete("v1/users/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var result = await userService.DeleteUser(id);
        if (!result.Success)
            return Conflict(new { erros = result.Errors });
        return Ok($"{result.Data.Name} - usuario deletado");
    }
    
}