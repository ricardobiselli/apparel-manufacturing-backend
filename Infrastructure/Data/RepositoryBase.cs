using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryBase<T> where T : class
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync() ?? new List<T>();

        }

        public async Task<T?> GetByIdAsync<Tid>(Tid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync<Tid>(Tid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<T?> UpdateAsync<Tid>(Tid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

    }
}
