using System.ComponentModel.DataAnnotations.Schema;

public class EditorEntity
{
    [Column("EditorID")]
    public int Id { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("EditorBio")]
    public string EditorBio { get; set; }
}