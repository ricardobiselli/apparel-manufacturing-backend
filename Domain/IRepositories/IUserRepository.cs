using Domain.Models;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmployeeNumberAsync(string employeeNumber);
        Task<User?> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
