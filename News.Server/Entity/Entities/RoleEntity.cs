using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace News.Server.Entity
{
    public class RoleEntity
    {
        [Column("RoleID")]
        public int Id { get; set; }

        [Column("RoleName")]
        public string RoleName { get; set; }
    }
    
}