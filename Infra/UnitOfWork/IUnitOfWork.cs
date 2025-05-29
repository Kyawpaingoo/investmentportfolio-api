using Data;
using Infra.Repository;

namespace Infra.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<tbUser> userRepo { get; }
    IRepository<tbAsset> assetRepo { get; }
    IRepository<tbRefreshToken> refreshTokenRepo { get; }
}