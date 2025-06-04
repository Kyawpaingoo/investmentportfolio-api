using API.Services.TransactionService;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("createTransaction")]
    public async Task<ActionResult<TransactionResponseDto>> CreateTransaction([FromBody] TransactionRequestDto dto)
    {
        var result = await _transactionService.CreateTransaction(dto);
        return Ok(result);
    }
}