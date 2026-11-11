using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniJiraApi.Application.Abstractions.Authentication;
using MiniJiraApi.Infrastructure.Persistence;
using MiniJiraApi.Application.Abstractions.Persistence;
using MiniJiraApi.Application.Authentication;
using MiniJiraApi.Application.Boards;
using MiniJiraApi.Infrastructure.Services.Authentication;

namespace MiniJiraApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<IAppDbContext>(provider =>
            provider.GetRequiredService<AppDbContext>());
        
        services.AddScoped<BoardService>();
        
        services.AddScoped<IPasswordHasher, IdentityPasswordHasher>();
        
        services.AddScoped<AuthService>();

        return services;
    }
}