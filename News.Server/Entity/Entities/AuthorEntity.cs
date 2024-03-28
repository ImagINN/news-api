using System.ComponentModel.DataAnnotations.Schema;

namespace News.Server.Entity
{
    public class AuthorEntity
    {
        [Column("AuthorID")]
        public int Id { get; set; }

        [Column("RoleID")]
        public int RoleId { get; set; }

        [Column("UserID")]
        public int UserId { get; set; }
        
        [Column("AuthorBio")]
        public string AuthorBio { get; set; }
    }
}