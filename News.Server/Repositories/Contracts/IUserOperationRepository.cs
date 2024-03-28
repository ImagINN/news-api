using News.Server.Entity;

    public interface IUserOperationRepository
    {
        Task<string> CreateUser(UserEntity userEntity);
        Task<string> RegisterUser(UserEntity userEntity);
        Task<string> UpdateUserPassword(UserPasswordEntity userPasswordEntity, int user_id, string user_password);
        Task<string> UpdateUserStatus(UserUpdateStatusEntity userUpdateStatusEntity, int user_id, int status_id);
        Task<string> UpdateUser(UserEntity userEntity, int user_id, int role_id, string user_name, string user_surname, 
                            string user_password, string user_email, int status_id, string user_pp_url);
    }
