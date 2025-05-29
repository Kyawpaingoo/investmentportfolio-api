using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

[Table("tbRefreshToken")]
public class tbRefreshToken
{
    [Key]
    public int ID { get; set; }
    public string AppID { get; set; }
    public string TokenValue { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Boolean? Revoked { get; set; }  
    public DateTime? RevokedDate { get; set; }
}