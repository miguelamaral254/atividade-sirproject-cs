using System.Collections.Generic;
using System.Threading.Tasks;
using SirProject.Core.Pagination; 

namespace SirProject.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<PaginatedList<T>> GetAllPaginatedAsync(int pageNumber, int pageSize); 
    }
}
