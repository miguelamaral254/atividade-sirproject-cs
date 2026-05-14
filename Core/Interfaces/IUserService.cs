using SirProject.Core.Entities;
using SirProject.Core.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirProject.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<PaginatedList<User>> GetAllPaginatedAsync(int pageNumber, int pageSize);
        Task<User?> GetByIdAsync(int id);
        Task<int> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}
