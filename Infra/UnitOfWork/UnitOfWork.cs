using Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private InvestmentPortfolioDBContext _ctx;

    public UnitOfWork(InvestmentPortfolioDBContext ctx)
    {
        _ctx = ctx;
    }

    ~UnitOfWork()
    {
        _ctx.Dispose();
    }
}