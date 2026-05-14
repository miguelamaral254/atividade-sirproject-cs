using SirProject.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SirProject.Core.Pagination; 

namespace SirProject.Core.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<Pessoa?> GetByIdAsync(int id);
        Task<int> CreateAsync(Pessoa pessoa);
        Task<bool> UpdateAsync(Pessoa pessoa);
        Task<bool> DeleteAsync(int id);
        Task<PaginatedList<Pessoa>> GetAllPaginatedAsync(int pageNumber, int pageSize); 
    }
}
