using Data;

namespace API.Services;

public interface IUserService
{
    Task<tbUser> Upsert(tbUser user);
}