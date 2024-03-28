using System.ComponentModel.DataAnnotations;

public class ArticleWithHoleInfoEntity
{
    [Key]
    public int ArticleID { get; set; }
    public string ArticleTitle { get; set; }
    //public string ArticleContent { get; set; }
    public string CategoryName { get; set; }
    //public string AuthorName { get; set; }
    //public string EditorName { get; set; }
    // public string PermissionName { get; set; }
    // public string StatusName { get; set; }
    public string ArticleImageURL { get; set; }
    public DateTime ArticlePublishedDate { get; set; }
    public int ArticleView { get; set; }
    // public string PreviousStatusName { get; set; }
    // public DateTime ArticleStatusModifiedDate { get; set; }
}