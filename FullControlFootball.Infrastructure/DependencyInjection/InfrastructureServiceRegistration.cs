using System.Text;
using FullControlFootball.Application.Abstractions.Authentication;
using FullControlFootball.Application.Abstractions.Persistence;
using FullControlFootball.Application.Abstractions.Time;
using FullControlFootball.Application.Features.CareerSaves.Services;
using FullControlFootball.Application.Features.CompetitionStandings.Services;
using FullControlFootball.Application.Features.Rankings.Services;
using FullControlFootball.Application.Features.Seasons.Services;
using FullControlFootball.Application.Features.Transfers.Services;
using FullControlFootball.Infrastructure.Authentication.Google;
using FullControlFootball.Infrastructure.Authentication.Jwt;
using FullControlFootball.Infrastructure.Authentication.Passwords;
using FullControlFootball.Infrastructure.Authentication.Services;
using FullControlFootball.Infrastructure.Persistence;
using FullControlFootball.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FullControlFootball.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.Configure<GoogleAuthSettings>(configuration.GetSection(GoogleAuthSettings.SectionName));

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICareerSaveService, CareerSaveService>();
        services.AddScoped<ISeasonService, SeasonService>();
        services.AddScoped<ICompetitionStandingService, CompetitionStandingService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddScoped<IRankingQueryService, RankingQueryService>();

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
            ?? throw new InvalidOperationException("JWT settings are missing.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });

        services.AddAuthorization();

        return services;
    }
}
