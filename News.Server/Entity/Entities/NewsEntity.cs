using System.ComponentModel.DataAnnotations.Schema;

/// summary
/// 
/// This class contains the properties of a news. These properties represent the columns of the "Articles" table within the "NewsProject" database. 
/// "[Column("Column_Name")]" tagging is done to match the tables in the database with the names of the created features.
/// 
/// Bu sınıf bir haberin özelliklerini içerir. Bu özellikler "NewProject" veritabanındaki "News" tablosunun sütunlarını temsil eder.
/// "[Column("Column_Name")]" etiketleme, veritabanındaki tabloları oluşturulan özelliklerin adlarıyla eşleştirmek için yapılır.
/// 
/// summary

namespace News.Server.Entity
{
    public class NewsEntity
    {
        [Column("ArticleID")]
        public int Id { get; set; }

        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Column("StatusID")]
        public int StatusId { get; set; }

        [Column("AuthorID")]
        public int AuthorId { get; set; }

        [Column("EditorID")]
        public int EditorId { get; set; }

        [Column("PermissionID")]
        public int PermissionId { get; set; }

        [Column("ArticleTitle")]
        public string Title { get; set; }

        [Column("ArticleContent")]
        public string Content { get; set; }

        [Column("ArticleImageURL")]
        public string ImageUrl { get; set; }

        [Column("ArticlePublishedDate")]
        public DateTime PublishedDate { get; set; }

        [Column("ArticleView")]
        public int Views { get; set; }

        [Column("ArticlePreviousStatusID")]
        public int PreviousStatusID { get; set; }

        [Column("ArticleStatusModifiedDate")]
        public DateTime StatusModifiedDate { get; set; }

        [Column("ArticleShow")]
        public string ArticleShow { get; set; }
    }
}