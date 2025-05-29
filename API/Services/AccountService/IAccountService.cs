using Data;
using Data.Dtos;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Services.AccountService;

public interface IAccountService
{
    Task<string> CreateAccount(CreateAccountRequest request);
    Task<AuthResponse> Login(AuthRequest authRequest);
    Task<AuthResponse> GenerateNewToken(string? refreshToken);
}