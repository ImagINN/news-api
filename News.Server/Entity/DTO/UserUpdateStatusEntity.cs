using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class UserUpdateStatusEntity
{
    [Key]
    public int UserId { get; set; }
    public int StatusId { get; set; }
}