using Domain.IRepositories;
using Domain.Models.Users;

namespace Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
    }
}
