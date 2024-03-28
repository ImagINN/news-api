using System.ComponentModel.DataAnnotations;

public class SearchArticlesEntity
{
    [Key]
    public int ArticleID { get; set; }
    public string ArticleTitle { get; set; }
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string ArticleImageURL { get; set; }
    public DateTime ArticlePublishedDate { get; set; }
    public int ArticleView { get; set; }
}