namespace Data.Dtos;

public struct ReturnMessage
{
    public const string Success = "Success";
    public const string Fail = "Fail";
}

public struct LoginMessage
{
    public const string NoUserExisted = "User does not existed";
    public const string IncorrectPassword = "Incorrect Password";
    public const string SuccessLogin = "Success";
    public const string FailLogin = "Fail to Login";
}