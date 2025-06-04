namespace Data.Dtos;

public struct ReturnMessage
{
    public const string Success = "Success";
    public const string Fail = "Fail";
    public const string Duplicate = "Duplicated";
}

public struct LoginMessage
{
    public const string NoUserExisted = "User does not existed";
    public const string IncorrectPassword = "Incorrect Password";
    public const string SuccessLogin = "Success";
    public const string FailLogin = "Fail to Login";
}

public struct TransactionMessage
{
    public const string UserNotFound = "User does not exist";
    public const string InsufficientFunds = "Insufficient Funds";
    public const string TransactionSuccess = "Success";
    public const string TransactionFail = "Fail";
    public const string TransactionDuplicate = "Duplicated";
    public const string PorfolioNotFound = "Porfolio Not Found";
    public const string AssetNotFound = "Asset Not Found";
}

public struct TransactionType
{
    public const string Buy = "Buy";
    public const string Sell = "Sell";
    public const string Transfer = "Transfer";
}