namespace Data.Dtos;

public class CreateAccountRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthResponse
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string AppID { get; set; }
    public TokenResponse TokenResponse { get; set; }
    public string ReturnMessage { get; set; }

    public AuthResponse(tbUser user, TokenResponse? tokenResponse)
    {
        Username = user.UserName;
        Email = user.Email;
        AppID = user.AppId;
        ReturnMessage = user.ReturnMessage;
        TokenResponse = tokenResponse;
    }
}