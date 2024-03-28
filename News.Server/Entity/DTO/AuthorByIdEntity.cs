using System.ComponentModel.DataAnnotations;

public class AuthorByIdEntity
{
    [Key]
    public int AuthorID { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public string AuthorBio { get; set; }
    public string UserPPUrl { get; set; }
}