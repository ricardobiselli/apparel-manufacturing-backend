using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GarmentRepository : RepositoryBase<Garment>, IGarmentRepository
    {
        private readonly ApplicationDbContext _context;

        public GarmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Garment>> GetAllGarmentsWithOperationsIncludedAsync()
        {
            return await _context.Set<Garment>()
                .Include(g => g.Operations)
                .ToListAsync();
        }

    }
}
