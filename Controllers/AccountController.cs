using EComerceApi.Extensions;
using EComerceApi.Service.ControllerSevice;
using EComerceApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EComerceApi.Controller;

[ApiController]
[Route("v1/accounts")]
public class AccountController(AccountService accountService): ControllerBase
{
    /// <summary>
    /// Cria uma nova conta de usuário.
    /// </summary>
    /// <param name="model">Dados para registro do usuário.</param>
    /// <returns>Resultado da criação da conta.</returns>
    /// <remarks>
    /// Exemplo de requisição:
    /// POST /v1/accounts
    /// {
    ///     "name": "João",
    ///     "email": "joao@email.com",
    ///     "password": "123456"
    /// }
    /// </remarks>
    /// <response code="200">Conta criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="409">Usuário já existente ou erro de conflito.</response>
    [HttpPost("registers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var result = await accountService.CreateAccount(model);
        if (!result.Success)
            return Conflict(new { erros = result.Errors });
        return Ok(result.Data);
        
    }
    /// <summary>
    /// Realiza o login de um usuário e retorna um token JWT.
    /// </summary>
    /// <param name="login">Credenciais do usuário.</param>
    /// <returns>Token JWT se o login for bem-sucedido.</returns>
    /// <remarks>
    /// Exemplo de requisição:
    /// POST /v1/accounts/logins
    /// {
    ///     "email": "joao@email.com",
    ///     "password": "123456"
    /// }
    /// </remarks>
    /// <response code="200">Login bem-sucedido.</response>
    /// <response code="401">Credenciais inválidas.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginViewModel login)
    {
        var token = await accountService.Login(login);
        return Ok(token);
    }
    
}