using FullControlFootball.Api.Extensions;
using FullControlFootball.Api.Middleware;
using FullControlFootball.Application.Common.Security;
using FullControlFootball.Infrastructure.DependencyInjection;
using FullControlFootball.Infrastructure.Authentication.Jwt;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext();
});

builder.Services.AddControllers();

builder.Services.AddApiServices();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserAccessor, HttpContextCurrentUserAccessor>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

internal sealed class HttpContextCurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextCurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetRequiredUserId()
    {
        var value = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? _httpContextAccessor.HttpContext?.User.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("Authenticated user identifier was not found.");

        return Guid.Parse(value);
    }
}
