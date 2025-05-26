using Microsoft.EntityFrameworkCore;

namespace Data;

public class InvestmentPortfolioDBContext : DbContext
{
    public InvestmentPortfolioDBContext(DbContextOptions<InvestmentPortfolioDBContext> options) : base(options)
    {
        
    }
    
    
}