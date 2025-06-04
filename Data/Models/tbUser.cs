using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Data;

[Table("tbUsers")]
public class tbUser
{
    [Key]
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? AppId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime? CreatedAt { get; set; }
    [Column(TypeName = "timestamp")]
    public bool? IsDeleted { get; set; }
    [NotMapped]
    public string? ReturnMessage { get; set; }
}