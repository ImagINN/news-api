using System.ComponentModel.DataAnnotations;

public class ArticleUpdateForEditor
{
    [Key]
    public int ArticleID { get; set; }
    public string ArticleShow { get; set; }
    public int StatusID { get; set; }
}