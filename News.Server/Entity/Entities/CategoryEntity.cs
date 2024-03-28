using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace News.Server.Entity
{
    public class CategoryEntity
    {
        [Column("CategoryID")]
        public int Id { get; set; }

        [Column("CategoryName")]
        public string CategoryName { get; set; }
    }
}
