using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

[Table("tbTransaction")]
public class tbTransaction
{
    [Key]
    public Guid ID { get; set; }
    [ForeignKey("tbPortfolio")]
    public Guid PortfolioID { get; set; }
    [ForeignKey("tbAsset")]
    public Guid AssetID { get; set; }
    public string? Type { get; set; }
    public string? Currency { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? PricePerUnit { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime? DateTime { get; set; }
}