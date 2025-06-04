using API.Services.PortfolioService;
using Data;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/portfolio")]
public class PortfolioController : ControllerBase
{
    private IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }    
    
    [HttpPost("createportfolio")]
    public async Task<IActionResult> Upsert(CreatePortfolioResponseDto portfolio)
    {
        var result = await _portfolioService.CreatePortfolio(portfolio);
        return Ok(result);
    }
    
    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _portfolioService.GetById(id);
        return Ok(result);
    }
    
    [HttpGet("getlistbyuserid")]
    public async Task<IActionResult> GetListByUserId(Guid userId)
    {
        var result = await _portfolioService.GetListByUserId(userId);
        return Ok(result);
    }
}