public class UserWithAdminEntity
{
    public int AdminId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public string UserEmail { get; set; }
    public DateTime UserRegistrationDate { get; set; }
}