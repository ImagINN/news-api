using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace News.Server.Entity
{
    public class UserEntity
    {
        [Column("UserID")]
        public int Id { get; set; }

        [Column("RoleID")]
        public int RoleId { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("UserSurname")]
        public string UserSurname { get; set; }

        [Column("UserPassword")]
        public string UserPassword { get; set; }

        [Column("UserEmail")]
        public string UserEmail { get; set; }

        [Column("UserRegistrationDate")]
        public DateTime UserRegistrationDate { get; set; }

        [Column("StatusID")]
        public int StatusID { get; set; }

        [Column("UserPPUrl")]
        public string UserPPUrl { get; set; }
    }
}
