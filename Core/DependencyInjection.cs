using Microsoft.Extensions.DependencyInjection;
using SirProject.Core.Interfaces;
using SirProject.Core.Services;

namespace SirProject.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
