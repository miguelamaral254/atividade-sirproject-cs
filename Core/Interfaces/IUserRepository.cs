using SirProject.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SirProject.Core.Pagination;

namespace SirProject.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<int> CreateAsync(User entity);
        Task<bool> UpdateAsync(User entity);
        Task<bool> DeleteAsync(int id);
        Task<PaginatedList<User>> GetAllPaginatedAsync(int pageNumber, int pageSize);
    }
}
