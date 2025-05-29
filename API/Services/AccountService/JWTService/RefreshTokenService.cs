using Core.Extension;
using Data;
using Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace API.Services.AccountService.JWTService;

public class RefreshTokenService : IRefreshTokenService
{
    IUnitOfWork _uow;

    public RefreshTokenService(IUnitOfWork uow)
    {
        this._uow = uow;
    }

    public async Task<tbRefreshToken> GenerateToken(string userAppID)
    {
        tbRefreshToken refreshToken = new tbRefreshToken();
        refreshToken.TokenValue = MyExtension.getUniqueCode();
        refreshToken.AppID = userAppID;
        refreshToken.ExpirationDate = DateTime.UtcNow.AddMinutes(5);
        refreshToken.Revoked = false;

        try
        {
            refreshToken = await _uow.refreshTokenRepo.InsertReturnAsync(refreshToken);
            return refreshToken;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<tbRefreshToken> RevokeToken(tbRefreshToken refreshToken)
    {
        if (refreshToken.ID > 0)
        {
            refreshToken.Revoked = true;
            refreshToken.RevokedDate = DateTime.UtcNow;
            
            refreshToken = await _uow.refreshTokenRepo.UpdateAsync(refreshToken);
        }
        return refreshToken;
    }

    public async Task<bool> ValidateRefreshToken(string refreshToken)
    {
        tbRefreshToken retrievedToken = await _uow.refreshTokenRepo.GetAll().Where(rt => rt.TokenValue.Equals(refreshToken)).FirstOrDefaultAsync();

        if (retrievedToken is null)
        {
            return false;
        }

        if (retrievedToken.Revoked is true) // DateTime.UtcNow < retrievedToken.ExpirationDate
        {
            return false;
        }
        return true;
    }
}