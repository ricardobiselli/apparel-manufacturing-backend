using Domain.Models.Users;

namespace Domain.IRepositories
{
    public interface IUserRepository 
    {
        Task<User> GetUserByEmailOrUsername(string emailOrUsername);
        public bool ExistsByUserName(string userName);
        public bool ExistsByEmail(string email);


    }
}
