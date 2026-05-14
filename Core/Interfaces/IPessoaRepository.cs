using SirProject.Core.Entities;
using System.Threading.Tasks;

namespace SirProject.Core.Interfaces
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<Pessoa?> GetByEmailAsync(string email);
    }
}
