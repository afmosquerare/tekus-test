using Application.Data;
using Domain.Aggregates.Providers;
using Domain.Primitives;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    
    public static IServiceCollection AddInsfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddPersistence( config );
        return services;
    } 

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>( 
            options => options.UseSqlServer( config.GetConnectionString("Database")));

        services.AddScoped<IApplicationDbContext>(
            sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(
            sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IProviderRepository, ProvidersRepository>();

        
        return services;
    }
}