using System.ComponentModel.DataAnnotations;

public class ArticleDetailsByIdEntity
{
    [Key]
    public int ArticleID { get; set; }
    public string ArticleTitle { get; set; }
    public string ArticleContent { get; set; }
    public string ArticleImageURL { get; set; }
    public DateTime ArticlePublishedDate { get; set; }
    public int ArticleView { get; set; }
    public string CategoryName { get; set; }
    public int AuthorID { get; set; }
    public string Author { get; set; }
}