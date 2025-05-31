using Data;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private InvestmentPortfolioDBContext _ctx;

    private IRepository<tbUser> _userRepo;
    private IRepository<tbAsset> _assetRepo;
    private IRepository<tbRefreshToken> _refreshTokenRepo;
    private IRepository<tbPortfolio> _portfolioRepo;
    
    public UnitOfWork(InvestmentPortfolioDBContext ctx)
    {
        _ctx = ctx;
    }

    ~UnitOfWork()
    {
        _ctx.Dispose();
    }

    public IRepository<tbUser> userRepo
    {
        get
        {
            if (_userRepo == null)
            {
                _userRepo = new Repository<tbUser>(_ctx);
            }
            return _userRepo;
        }
    }

    public IRepository<tbAsset> assetRepo
    {
        get
        {
            if (_assetRepo == null)
            {
                _assetRepo = new Repository<tbAsset>(_ctx);
            }
            return _assetRepo;
        }
    }
    
    public IRepository<tbRefreshToken> refreshTokenRepo
    {
        get
        {
            if (_refreshTokenRepo == null)
            {
                _refreshTokenRepo = new Repository<tbRefreshToken>(_ctx);
            }
            return _refreshTokenRepo;
        }
    }

    public IRepository<tbPortfolio> portfolioRepo
    {
        get
        {
            if (_portfolioRepo == null)
            {
                _portfolioRepo = new Repository<tbPortfolio>(_ctx);
            }
            return _portfolioRepo;
        }
    }
}