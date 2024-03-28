using System.ComponentModel.DataAnnotations;

public class LoginEntity
{
    [Key]
    public int UserID { get; set; }
    public int RoleID { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
}