using Lab10_AlexandroCano.Application.Interfaces.Security;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Infrastructure.Persistence;
using Lab10_AlexandroCano.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkImplementation = Lab10_AlexandroCano.Infrastructure.UnitOfWork.UnitOfWork;

namespace Lab10_AlexandroCano.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWorkImplementation>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
