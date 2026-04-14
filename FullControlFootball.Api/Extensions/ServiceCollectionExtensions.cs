using FluentValidation;
using FluentValidation.AspNetCore;
using FullControlFootball.Application.Features.Auth.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace FullControlFootball.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        return services;
    }
}
