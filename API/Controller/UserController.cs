using API.Services;
using Data;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    IUserService _iuserService;

    public UserController(IUserService iuserService)
    {
        this._iuserService = iuserService;
    }

    [HttpPost("upsert")]
    public async Task<ActionResult<tbUser>> Upsert([FromBody] tbUser user)
    {
        var result = await _iuserService.Upsert(user);
        return Ok(result);
    }
}