using Data;
using Data.Dtos;

namespace API.Services.AccountService.JWTService;

public interface IJWTAuthService
{
    Task<TokenResponse> GenerateTokenResponse(tbUser user);
}