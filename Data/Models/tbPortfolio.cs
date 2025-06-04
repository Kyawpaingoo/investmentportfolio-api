using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

[Table("tbPortfolio")]
public class tbPortfolio
{
    [Key]
    public Guid ID { get; set; }
    public string? PortfolioName { get; set; }
    public decimal? TotalIncome { get; set; }
    [ForeignKey("tbUser")]
    public Guid UserID { get; set; }
    public bool? IsDeleted { get; set; }
    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }
}