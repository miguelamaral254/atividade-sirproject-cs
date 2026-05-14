using Microsoft.EntityFrameworkCore;
using SirProject.Core.Entities;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirProject.Infrastructure.Data
{
    public class EfUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public EfUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<int> CreateAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Users.AnyAsync(e => e.Id == entity.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedList<User>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            return await PaginatedList<User>.CreateAsync(_context.Users.AsNoTracking(), pageNumber, pageSize);
        }
    }
}
