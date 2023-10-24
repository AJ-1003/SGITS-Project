using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SGITS.App.Interfaces;
using SGITS.Persistence.Repositories;

namespace SGITS.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
        });

        services.AddTransient<IFamilyMemberRepository, FamilyMemberRepository>();
        services.AddTransient<IHouseRepository, HouseRepository>();

        return services;
    }
}
