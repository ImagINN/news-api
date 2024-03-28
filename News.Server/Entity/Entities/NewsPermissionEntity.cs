using System.ComponentModel.DataAnnotations.Schema;

public class NewsPermissionEntity
{
    [Column("PermissionID")]
    public int Id { get; set; }

    [Column("PermissionName")]
    public string PermissionName { get; set; }
}