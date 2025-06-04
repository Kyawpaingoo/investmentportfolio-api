using Core.Extension;
using Data;
using Data.Dtos;
using Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API.Services.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly InvestmentPortfolioDBContext _context;
    private IUnitOfWork _uow;
    private DateTime now;

    public TransactionService(InvestmentPortfolioDBContext context)
    {
        _context = context;
        _uow = new UnitOfWork(context);
        now = MyExtension.getUtcTime();
    }
    
    public async Task<TransactionResponseDto> CreateTransaction(TransactionRequestDto dto)
    {
        var user = await _uow.userRepo.GetWithoutTracking().
                        Where(a => a.Id == dto.UserId && a.IsDeleted != true)
                            .FirstOrDefaultAsync();
        if(user is null)
        {
            return new TransactionResponseDto() {ReturnMessage = TransactionMessage.UserNotFound};
        }
        
        var asset = await _uow.assetRepo.GetWithoutTracking()
            .Where(a => a.ID == dto.AssetId && a.IsDeleted != true).FirstOrDefaultAsync();
        
        if (asset is null)
        {
            return new TransactionResponseDto() {ReturnMessage = TransactionMessage.AssetNotFound};
        }
        
        var portfolio = await _uow.portfolioRepo.GetAll().
                                Where(a => a.ID == dto.PortfolioId && a.IsDeleted != true)
                                .FirstOrDefaultAsync();
        if (portfolio is null)
        {
            return new TransactionResponseDto() {ReturnMessage = TransactionMessage.PorfolioNotFound};
        }

        // if (dto.TransactionType == TransactionType.Sell)
        // {
        //     
        // }
        
        var transaction = new tbTransaction()
        {
            ID = new Guid(),
            PortfolioID = dto.PortfolioId,
            AssetID = dto.AssetId,
            TransactionType = dto.TransactionType,
            TransactionID = MyExtension.getUniqueCode(),
            Currency = dto.Currency,
            Quantity = dto.Quantity,
            PricePerUnit = dto.PricePerUnit,
            TotalPrice = dto.PricePerUnit * dto.Quantity,
            AccessTime = now
        };
        
        var trantxtResult = await _uow.transactionRepo.InsertReturnAsync(transaction);

        if (trantxtResult is null)
        {
            return new TransactionResponseDto() { ReturnMessage = TransactionMessage.TransactionFail };
        }

        portfolio.TotalIncome = dto.TransactionType == TransactionType.Buy 
                                ? portfolio.TotalIncome + trantxtResult.TotalPrice
                                : portfolio.TotalIncome - trantxtResult.TotalPrice;

        var portfolioResult = await _uow.portfolioRepo.UpdateAsync(portfolio);
        
        if (portfolioResult is null)
        {
            return new TransactionResponseDto() { ReturnMessage = TransactionMessage.TransactionFail };
        }

        var result = new TransactionResponseDto()
        {
            AssetName = asset.Name,
            PortfolioName = portfolio.PortfolioName,
            TransactionID = trantxtResult.TransactionID,
            TransactionType = trantxtResult.TransactionType,
            Currency = trantxtResult.Currency,
            Quantity = trantxtResult.Quantity,
            PricePerUnit = trantxtResult.PricePerUnit,
            TotalPrice = trantxtResult.TotalPrice,
            AccessTime = now,
            ReturnMessage = TransactionMessage.TransactionSuccess
        };
        
        return result;
    }
}