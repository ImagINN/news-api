using System.ComponentModel.DataAnnotations.Schema;

public class AdminEntity
{
    [Column("AdminID")]
    public int Id { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }
}