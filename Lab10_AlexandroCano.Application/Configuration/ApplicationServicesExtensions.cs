using Lab10_AlexandroCano.Application.Interfaces.Services;
using Lab10_AlexandroCano.Application.Services;
using Lab10_AlexandroCano.Application.UseCases.Auth;
using Lab10_AlexandroCano.Application.UseCases.Responses;
using Lab10_AlexandroCano.Application.UseCases.Tickets;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10_AlexandroCano.Application.Configuration;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<LoginUseCase>();

        services.AddScoped<GetAllTicketsUseCase>();
        services.AddScoped<GetTicketByIdUseCase>();
        services.AddScoped<CreateTicketUseCase>();
        services.AddScoped<UpdateTicketStatusUseCase>();
        services.AddScoped<DeleteTicketUseCase>();

        services.AddScoped<GetAllResponsesUseCase>();
        services.AddScoped<GetResponseByIdUseCase>();
        services.AddScoped<GetResponsesByTicketUseCase>();
        services.AddScoped<CreateResponseUseCase>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IResponseService, ResponseService>();

        return services;
    }
}
