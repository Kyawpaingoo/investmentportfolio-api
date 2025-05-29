using Data;

namespace API.Services.AccountService.JWTService;

public interface IRefreshTokenService
{
    Task<tbRefreshToken> GenerateToken(string userAppID);
    Task<tbRefreshToken> RevokeToken(tbRefreshToken refreshToken);
    Task<bool> ValidateRefreshToken(string refreshToken);
}