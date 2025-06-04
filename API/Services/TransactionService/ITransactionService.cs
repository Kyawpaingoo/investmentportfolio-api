using Data.Dtos;

namespace API.Services.TransactionService;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateTransaction(TransactionRequestDto dto);
    
}