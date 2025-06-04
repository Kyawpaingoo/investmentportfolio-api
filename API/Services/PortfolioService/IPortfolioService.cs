using Data;
using Data.Dtos;

namespace API.Services.PortfolioService;

public interface IPortfolioService
{
    Task<string> CreatePortfolio(CreatePortfolioResponseDto portfolio);
    Task<tbPortfolio> GetById(Guid id);
    Task<List<tbPortfolio>> GetListByUserId(Guid userId);
}