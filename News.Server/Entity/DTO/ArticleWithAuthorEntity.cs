using System.ComponentModel.DataAnnotations;

public class ArticleWithAuthorEntity
{
    [Key]
    public int ArticleID { get; set; }
    public string ArticleTitle { get; set; }
    public string ArticleContent { get; set; }
    public int AuthorID { get; set; }
    public string Author { get; set; }
    public int EditorID { get; set; }
    public string Editor { get; set; }
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string ArticleImageURL { get; set; }
    public DateTime ArticlePublishedDate { get; set; }
    public int ArticleView { get; set; }
    public int StatusID { get; set; }
    public string StatusName { get; set; }
    public int ArticlePreviousStatusID { get; set; }
    public string PreviousStatusName { get; set; }
    public DateTime ArticleStatusModifiedDate { get; set; }
    public int PermissionID { get; set; }
    public string PermissionName { get; set; }
    public string ArticleShow { get; set; }
}