namespace Data.Dtos;

public class TransactionRequestDto
{
    public Guid UserId { get; set; }
    public Guid AssetId { get; set; }
    public Guid PortfolioId { get; set; }
    public string? TransactionType { get; set; }
    public string? Currency { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? PricePerUnit { get; set; }
}

public class TransactionResponseDto
{
    public string? AssetName { get; set; }
    public string? PortfolioName { get; set; }
    public string? TransactionID { get; set; }
    public string? TransactionType { get; set; }
    public string? Currency { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? PricePerUnit { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime? AccessTime { get; set; }
    public string? ReturnMessage { get; set; }
}