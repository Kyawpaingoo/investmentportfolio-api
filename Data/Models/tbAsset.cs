using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

[Table("tbAssets")]
public class tbAsset
{
    [Key]
    public Guid ID { get; set; }
    public string? Name { get; set; }
    public string? Symbol { get; set; }
    public string? Category { get; set; }
    public string? IconURL { get; set; }
    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }
    public bool? IsDeleted { get; set; }

    [NotMapped]
    public string? IconURLString
    {
        get
        {
            if (this.IconURL != null)
            {
                return this.IconURL;
            }
            return null;
        }
    }
}