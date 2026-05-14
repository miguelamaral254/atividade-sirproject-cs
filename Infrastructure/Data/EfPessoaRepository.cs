using Microsoft.EntityFrameworkCore;
using SirProject.Core.Entities;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirProject.Infrastructure.Data
{
    public class EfPessoaRepository : IPessoaRepository
    {
        private readonly ApplicationDbContext _context;

        public EfPessoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa?> GetByIdAsync(int id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<Pessoa?> GetByEmailAsync(string email)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<int> CreateAsync(Pessoa entity)
        {
            _context.Pessoas.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Pessoa entity)
        {
            var trackedEntity = _context.ChangeTracker.Entries<Pessoa>()
                .FirstOrDefault(e => e.Entity.Id == entity.Id);
            
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Pessoas.AnyAsync(e => e.Id == entity.Id))
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
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return false;

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedList<Pessoa>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            return await PaginatedList<Pessoa>.CreateAsync(_context.Pessoas.AsNoTracking(), pageNumber, pageSize);
        }
    }
}
