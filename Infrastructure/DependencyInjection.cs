using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SirProject.Core.Interfaces;
using SirProject.Infrastructure.Data;

namespace SirProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
            var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") 
            ?? throw new InvalidOperationException("POSTGRES_CONNECTION_STRING environment variable not set.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IPessoaRepository, EfPessoaRepository>();
        services.AddScoped<IUserRepository, EfUserRepository>();

        return services;
    }
}
