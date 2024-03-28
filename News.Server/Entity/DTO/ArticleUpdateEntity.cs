using System.ComponentModel.DataAnnotations;

public class ArticleUpdateEntity
{
    [Key]
    public int ArticleID { get; set; }
    public int CategoryID { get; set; }
    public int PermissionID { get; set; }
    public string ArticleTitle { get; set; }
    public string ArticleContent { get; set; }
    public string ImageUrl { get; set; }
}