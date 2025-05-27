using Data;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private InvestmentPortfolioDBContext _ctx;

    private IRepository<tbUser> _userRepo;
    private IRepository<tbAsset> _assetRepo;

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
}