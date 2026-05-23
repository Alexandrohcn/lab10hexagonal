using Lab10_AlexandroCano.Application.Interfaces;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Lab10_AlexandroCano.Infrastructure.Persistence;
using Lab10_AlexandroCano.Infrastructure.Services;
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

        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}