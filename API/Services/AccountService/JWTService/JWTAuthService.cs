using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Data.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.AccountService.JWTService;

public class JWTAuthService : IJWTAuthService
{
    IRefreshTokenService _refreshTokenService;

    public JWTAuthService(IRefreshTokenService refreshTokenService)
    {
        this._refreshTokenService = refreshTokenService;
    }

    public async Task<TokenResponse> GenerateTokenResponse(tbUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("my-super-secret-key-kpo-123456-123456!");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.AppId)
            }),
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        var accessToken = tokenHandler.CreateToken(tokenDescriptor);
        
        var refreshToken = await _refreshTokenService.GenerateToken(user.AppId);
        return new TokenResponse()
        {
            AccessToken = tokenHandler.WriteToken(accessToken),
            RefreshToken = refreshToken.TokenValue,
            ExpiresAt = tokenDescriptor.Expires.Value,
        };
    }
}