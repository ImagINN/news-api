public class UserWithRoleEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    //public string UserPassword { get; set; }
    public string UserEmail { get; set; }
    public DateTime UserRegistrationDate { get; set; }
    public int StatusID { get; set; }
    public string StatusName { get; set; }
}