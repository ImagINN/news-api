using System.ComponentModel.DataAnnotations;

public class ArticleTopViewEntity
{
    [Key]
    public int ArticleID { get; set; }
    public string ArticleTitle { get; set; }
    public int ArticleView { get; set; }
    public string ArticleImageURL { get; set; }
}