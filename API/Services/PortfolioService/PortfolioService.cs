using Core.Extension;
using Data;
using Data.Dtos;
using Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace API.Services.PortfolioService;

public class PortfolioService : IPortfolioService
{
    private readonly InvestmentPortfolioDBContext _context;
    private IUnitOfWork _uow;
    private DateTime now;

    public PortfolioService(InvestmentPortfolioDBContext context)
    {
        _context = context;
        _uow = new UnitOfWork(_context);
        now = MyExtension.getUtcTime();
    }
    
    public async Task<string> CreatePortfolio(CreatePortfolioResponseDto portfolio)
    {
        tbPortfolio data = new tbPortfolio()
        {
            ID = new Guid(),
            PortfolioName = portfolio.PortfolioName,
            UserID = portfolio.UserId,
            TotalIncome = 0,
            CreatedAt = now,
            IsDeleted = false,
        };
        
        var result = await _uow.portfolioRepo.InsertReturnAsync(data);
        

        return result is not null ? ReturnMessage.Success : ReturnMessage.Fail;
    }

    public async Task<tbPortfolio> GetById(Guid id)
    {
        var result = await _uow.portfolioRepo.GetWithoutTracking().
                                    Where(a => a.ID == id && a.IsDeleted != true)
                                        .FirstOrDefaultAsync() ?? new tbPortfolio();
        return result;
    }

    public async Task<List<tbPortfolio>> GetListByUserId(Guid userId)
    {
        var resultList = await _uow.portfolioRepo.GetWithoutTracking()
                        .Where(a => a.UserID == userId && a.IsDeleted != true).ToListAsync() ?? new List<tbPortfolio>();
        return resultList;
    }
}