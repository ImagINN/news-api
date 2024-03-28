using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using News.Server.Data;
using News.Server.Entity;

public class UserOperationRepository : IUserOperationRepository
{
    private readonly NewsDbContext newsDbContext;

    public UserOperationRepository(NewsDbContext newsDbContext)
    {
        this.newsDbContext = newsDbContext;
    }

    public async Task<string> CreateUser(UserEntity userEntity)
    {
        string Message = "İşlem Başarılı!";

        try
        {
            var items = await this.newsDbContext.Users
            .FromSqlRaw("EXEC dbo.SP_Create_User @role_id, @user_name, @user_surname, @user_password, @user_email, @status_id, @user_pp_url",
            new SqlParameter("@role_id", userEntity.RoleId),
            new SqlParameter("@user_name", userEntity.UserName),
            new SqlParameter("@user_surname", userEntity.UserSurname),
            new SqlParameter("@user_password", userEntity.UserPassword),
            new SqlParameter("@user_email", userEntity.UserEmail),
            new SqlParameter("@status_id", userEntity.StatusID),
            new SqlParameter("@user_pp_url", userEntity.UserPPUrl)).ToListAsync();
        }
        catch (System.Exception ex)
        {
            Message = "İşlem Başarısız! Sistem Mesajı: \n" + ex.Message;
        }

        return Message;
    }

    public async Task<string> RegisterUser(UserEntity userEntity)
    {
        string Message = "İşelem Başarılı!";

        try
        {
            var items = await this.newsDbContext.Users
                .FromSqlRaw("EXEC dbo.SP_Register_User @role_id, @user_name, @user_surname, @user_email",
                new SqlParameter("@role_id", userEntity.RoleId),
                new SqlParameter("@user_name", userEntity.UserName),
                new SqlParameter("@user_surname", userEntity.UserSurname),
                new SqlParameter("@user_email", userEntity.UserEmail)).ToListAsync();
        }
        catch (System.Exception ex)
        {
            Message = "İşlem Başarısız! Sistem Mesajı: \n" + ex.Message;
        }

        return Message;
    }

    public async Task<string> UpdateUser(UserEntity userEntity, int user_id, int role_id, string user_name, string user_surname, 
                            string user_password, string user_email, int status_id, string user_pp_url)
    {
        if (await UserExists(userEntity.Id) == false)
        {
            var items = await newsDbContext.Users
            .FromSqlRaw("EXEC dbo.SP_Update_User @user_id, @role_id, @user_name, @user_surname, @user_password, @user_email, @status_id, @user_pp_url",
            new SqlParameter("@user_id", user_id),
            new SqlParameter("@role_id", role_id),
            new SqlParameter("@user_name", user_name),
            new SqlParameter("@user_surname", user_surname),
            new SqlParameter("@user_password", user_password),
            new SqlParameter("@user_email", user_email),
            new SqlParameter("@status_id", status_id),
            new SqlParameter("@user_pp_url", user_pp_url)).ToListAsync();
        }

        return "İşlem yapıldı";
    }

    public async Task<string> UpdateUserPassword(UserPasswordEntity userPasswordEntity, int user_id, string user_password)
    {
        string Message = "İşlem Başarılı!\t";

        try
        {
            if (await UserExists(userPasswordEntity.UserId) == false)
            {
                var items = await newsDbContext.UpdateUserPassword
                .FromSqlRaw("EXEC dbo.SP_Update_User_Password @user_id, @user_password",
                new SqlParameter("@user_id", user_id),
                new SqlParameter("@user_password", user_password)).ToListAsync();
            }
        }
        catch (System.Exception ex)
        {
            return Message + ex.Message;
        }       

        return Message;
    }

    public async Task<string> UpdateUserStatus(UserUpdateStatusEntity userUpdateStatusEntity, int user_id, int status_id)
    {
        string Message = "İşlem başarılı.\t";

        try
        {
            if (await UserExists(userUpdateStatusEntity.UserId) == false) 
            {
                var items = await newsDbContext.UpdateUserStatus
                    .FromSqlRaw("EXEC dbo.SP_Update_User_Status @user_id, @status_id",
                    new SqlParameter("@user_id", user_id),
                    new SqlParameter("@status_id", status_id)).ToListAsync();
            }
        }
         catch (System.Exception ex)
        {
            return Message + ex.Message;
        }       

        return Message;
    }

    private async Task<bool> UserExists(int user_id)
    {
        return await this.newsDbContext.Users.AnyAsync(a => a.Id == user_id);
    }
}