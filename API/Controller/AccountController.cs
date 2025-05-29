using API.Services.AccountService;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        this._accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateAccount([FromBody]CreateAccountRequest request)
    {
        var result = await _accountService.CreateAccount(request);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]AuthRequest authRequest)
    {
        var result = await _accountService.Login(authRequest);
        return Ok(result);
    }

    [HttpPost("generatenewtoken")]
    public async Task<IActionResult> GenerateNewToken(string? refreshToken)
    {
        var result = await _accountService.GenerateNewToken(refreshToken);
        return Ok(result);
    }
}