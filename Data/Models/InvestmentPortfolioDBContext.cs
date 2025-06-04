using System.Reflection.Emit;
using Core.Extension;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class InvestmentPortfolioDBContext : DbContext
{
    public InvestmentPortfolioDBContext(DbContextOptions<InvestmentPortfolioDBContext> options) : base(options)
    {
        
    }
    
    public virtual DbSet<tbUser> tbUsers { get; set; }
    public virtual DbSet<tbAsset> tbAssets { get; set; }
    public virtual DbSet<tbRefreshToken> tbRefreshTokens { get; set; }
    public virtual DbSet<tbPortfolio> tbPortfolios { get; set; }
    public virtual DbSet<tbTransaction> tbTransactions { get; set; }
}