using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

public class UserStatusEntity
{
    [Key]
    [Column("StatusID")]
    public int UserStatusID { get; set; }

    [Column("StatusName")]
    public string UserStatusName { get; set; }
}