using SirProject.Core.Entities;
using System.Threading.Tasks;

namespace SirProject.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(string username, string password);
    }
}
