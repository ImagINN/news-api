//using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using News.Server.Data;
using News.Server.Entity;
using News.Server.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace News.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NewsDbContext newsDbContext;

        private string signinKey = ""; //Your signinKey

        public UserRepository(NewsDbContext newsDbContext)
        {
            this.newsDbContext = newsDbContext;
        }

        public async Task<IEnumerable<UserWithAdminEntity>> GetAllAdmins()
        {
            var item = await (from user in newsDbContext.Users
                            join admin in newsDbContext.Admins
                            on user.Id equals admin.UserId
                            select new UserWithAdminEntity
                            {
                                AdminId = admin.Id,
                                UserId = user.Id,
                                UserName = user.UserName,
                                UserSurname = user.UserSurname,
                                UserEmail = user.UserEmail,
                                UserRegistrationDate = user.UserRegistrationDate
                            }).ToListAsync();

            if (item != null && item.Any())
            {
                return item;
            }
            else
            {
                return Enumerable.Empty<UserWithAdminEntity>();
            }
        }

        public async Task<IEnumerable<UserWithAuthorEntity>> GetAllAuthors()
        {
            //var item = await newsDbContext.Authors.ToListAsync();
            var item = await (from user in newsDbContext.Users
                            join author in newsDbContext.Authors
                            on user.Id equals author.UserId
                            select new UserWithAuthorEntity
                            {
                                AuthorId = author.Id,
                                UserId = user.Id,
                                UserName = user.UserName,
                                UserSurname = user.UserSurname,
                                AuthorBio = author.AuthorBio,
                                UserEmail = user.UserEmail,
                                UserRegistrationDate = user.UserRegistrationDate
                            }).ToListAsync();

            if (item != null && item.Any())
            {
                return item;
            }
            else
            {
                return Enumerable.Empty<UserWithAuthorEntity>();
            }
        }

        public async Task<IEnumerable<UserWithEditorEntity>> GetAllEditors()
        {
            var item = await (from user in newsDbContext.Users
                            join editor in newsDbContext.Editors
                            on user.Id equals editor.UserId
                            select new UserWithEditorEntity
                            {
                                EditorId = editor.Id,
                                UserId = user.Id,
                                UserName = user.UserName,
                                UserSurname = user.UserSurname,
                                EditorBio = editor.EditorBio,
                                UserEmail = user.UserEmail,
                                UserRegistrationDate = user.UserRegistrationDate
                            }).ToListAsync();

            if (item != null && item.Any())
            {
                return item;
            }
            else
            {
                return Enumerable.Empty<UserWithEditorEntity>();
            }
        }

        public async Task<IEnumerable<RoleEntity>> GetAllRoles()
        {
            var item = await this.newsDbContext.Roles.ToListAsync();

            if (item != null && item.Any())
            {
                return item;
            }
            else
            {
                return Enumerable.Empty<RoleEntity>();
            }
        }

        public async Task<IEnumerable<UserWithRoleEntity>> GetAllUsers()
        {
            var item = await (from user in newsDbContext.Users
                            join role in newsDbContext.Roles 
                            on user.RoleId equals role.Id
                            join status in newsDbContext.UserStatus
                            on user.StatusID equals status.UserStatusID
                            select new UserWithRoleEntity
                            {
                                UserId = user.Id,
                                RoleId = role.Id,
                                RoleName = role.RoleName,
                                UserName = user.UserName,
                                UserSurname = user.UserSurname,
                                //UserPassword = user.UserPassword,
                                UserEmail = user.UserEmail,
                                UserRegistrationDate = user.UserRegistrationDate,
                                StatusID = user.StatusID,
                                StatusName = status.UserStatusName
                            }).ToListAsync();

            if (item != null && item.Any())
            {
                return item;
            }
            else
            {
                return Enumerable.Empty<UserWithRoleEntity>();
            }
        }

        public async Task<IEnumerable<UserStatusEntity>> GetAllUserStatuses()
        {
            var item = await this.newsDbContext.UserStatus.ToListAsync();

            if (item != null && item.Any())
            {
                return item;
            }
            else
            {
                return Enumerable.Empty<UserStatusEntity>();
            }
        }

        public async Task<IEnumerable<AuthorByIdEntity>> GetAuthorById(int author_id)
        {
            var item = await this.newsDbContext.AuthorById
                .FromSqlRaw("EXEC dbo.SP_Author_By_Id @author_id",
                new SqlParameter("@author_id", author_id)).ToListAsync();

            if (item != null && item.Any())
            {
                return item;
            }
            else
            {
                return Enumerable.Empty<AuthorByIdEntity>();
            }
        }

        public async Task<string> CheckedUser(string email, string password)
        {
            var item = await this.newsDbContext.Login
                .FromSqlRaw("EXEC dbo.SP_Login_User @email",
                new SqlParameter("@email", email)).ToListAsync();

            if (item != null && item[0].UserPassword == password)
            {
                var token = GetToken(item[0].UserID.ToString(), item[0].RoleID.ToString());
                return token;
            }
            else
            {
                return null;
            }
        }

        //public string GetToken(string userEmail, string userID, string authorID, string roleID)
        public string GetToken(string userID, string roleID)
        {
            var claims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Email, userEmail),
                new Claim("UserID", userID),
                //new Claim("AuthorID", authorID),
                new Claim("RoleID", roleID)
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));
            var credantials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: "https://www.haberto.com",
                audience: "https://www.haberto.com",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                notBefore: DateTime.Now,
                signingCredentials: credantials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }

    }
}
