using News.Server.Entity;

namespace News.Server.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserWithRoleEntity>> GetAllUsers();
        Task<IEnumerable<RoleEntity>> GetAllRoles();
        Task<IEnumerable<UserWithAuthorEntity>> GetAllAuthors();
        Task<IEnumerable<AuthorByIdEntity>> GetAuthorById(int author_id);
        Task<IEnumerable<UserWithEditorEntity>> GetAllEditors();
        Task<IEnumerable<UserWithAdminEntity>> GetAllAdmins();
        Task<IEnumerable<UserStatusEntity>> GetAllUserStatuses();
        Task<string> CheckedUser(string email, string password);
    }
}
