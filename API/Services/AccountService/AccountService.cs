using API.Services.AccountService.JWTService;
using Core.Extension;
using Data;
using Data.Dtos;
using Infra.UnitOfWork;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly InvestmentPortfolioDBContext _dbContext;
    private readonly IUnitOfWork _uow; 
    private readonly IJWTAuthService _jwtAuthService;
    DateTime now;

    public AccountService(InvestmentPortfolioDBContext dbContext, IJWTAuthService jwtAuthService)
    {
        _dbContext = dbContext;
        _uow = new UnitOfWork(dbContext);
        _jwtAuthService = jwtAuthService;
        now = MyExtension.getUtcTime();
    }

    public async Task<string> CreateAccount(CreateAccountRequest request)
    {
        var existedUser = await _uow.userRepo.GetAll().FirstOrDefaultAsync(x => x.Email == request.Email);
        if (existedUser != null)
        {
            return ReturnMessage.Duplicate;
        }
        
        tbUser user = new tbUser()
        {
            Id = new Guid(),
            UserName = request.Username,
            AppId = MyExtension.getUniqueCode(),
            Email = request.Email,
            Password = request.Password,
            CreatedAt = now,
            IsDeleted = false
        };
        
        var result = await _uow.userRepo.InsertReturnAsync(user);
        return result is not null ? ReturnMessage.Success : ReturnMessage.Fail;
    }

    public async Task<AuthResponse> Login(AuthRequest authRequest)
    {
        var user = await _uow.userRepo.GetWithoutTracking().Where(a => a.Email.Equals(authRequest.Email)).FirstOrDefaultAsync();
        if (user is null)
        {
            user = new tbUser() {ReturnMessage = LoginMessage.NoUserExisted};
            return new AuthResponse(user, null);
        }

        if (user.Password != authRequest.Password)
        {
            user = new tbUser() {ReturnMessage = LoginMessage.IncorrectPassword};
            return new AuthResponse(user, null);
        }
        
        TokenResponse tokenResponse = await _jwtAuthService.GenerateTokenResponse(user);
        if (tokenResponse is null)
        {
            user = new tbUser() {ReturnMessage = LoginMessage.FailLogin};
            return new AuthResponse(user, null);
        }
        user.ReturnMessage = LoginMessage.SuccessLogin;
        return new AuthResponse(user, tokenResponse);
    }

    public async Task<AuthResponse> GenerateNewToken(string? refreshToken)
    {
        var refreshTokenObj = await _uow.refreshTokenRepo.GetAll().FirstOrDefaultAsync(a => a.TokenValue == refreshToken && a.ExpirationDate >= DateTime.UtcNow) ?? new tbRefreshToken();
        var user = await _uow.userRepo.GetAll().Where(a => a.AppId == refreshTokenObj.AppID).FirstOrDefaultAsync();

        var tokenResponse = await _jwtAuthService.GenerateTokenResponse(user);
        return new AuthResponse(user, tokenResponse);
    }
}