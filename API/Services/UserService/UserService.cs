using Core.Extension;
using Data;
using Infra.UnitOfWork;

namespace API.Services;

public class UserService : IUserService
{
    private readonly InvestmentPortfolioDBContext _dbContext;
    private readonly IUnitOfWork _uow;
    DateTime now;

    public UserService(InvestmentPortfolioDBContext dbContext)
    {
        _dbContext = dbContext;
        _uow = new UnitOfWork(dbContext);
        now = MyExtension.getUtcTime();
    }

    public async Task<tbUser> Upsert(tbUser user)
    {
        tbUser result = new tbUser();
        if (user.Id != Guid.Empty)
        {
            user.IsDeleted = false;
            result = await _uow.userRepo.UpdateAsync(user);
        }
        else
        {
            user.Id = new Guid();
            user.CreatedAt = now;
            user.IsDeleted = false;
            user.AppId = MyExtension.getUniqueCode();

            result = await _uow.userRepo.InsertReturnAsync(user);
        }
        return result;
    }
}