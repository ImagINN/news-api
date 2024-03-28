using System.ComponentModel.DataAnnotations;

public class UserPasswordEntity
{
    [Key]
    public int UserId { get; set; }
    public string UserPassword { get; set; }
}