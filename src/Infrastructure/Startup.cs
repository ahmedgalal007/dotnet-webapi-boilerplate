using System.Reflection;
using System.Runtime.CompilerServices;
using Asp.Versioning;
using FSH.WebApi.Infrastructure.Auth;
using FSH.WebApi.Infrastructure.BackgroundJobs;
using FSH.WebApi.Infrastructure.Caching;
using FSH.WebApi.Infrastructure.Common;
using FSH.WebApi.Infrastructure.Cors;
using FSH.WebApi.Infrastructure.FileStorage;
using FSH.WebApi.Infrastructure.Localization;
using FSH.WebApi.Infrastructure.Mailing;
using FSH.WebApi.Infrastructure.Mapping;
using FSH.WebApi.Infrastructure.Middleware;
using FSH.WebApi.Infrastructure.Multitenancy;
using FSH.WebApi.Infrastructure.Notifications;
using FSH.WebApi.Infrastructure.OpenApi;
using FSH.WebApi.Infrastructure.Persistence;
using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using FSH.WebApi.Infrastructure.SecurityHeaders;
using FSH.WebApi.Infrastructure.SEO;
using FSH.WebApi.Infrastructure.Validations;
using FSH.WebApi.Infrastructure.Workflow;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Infrastructure.Test")]

namespace FSH.WebApi.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(FSH.WebApi.Application.Startup).GetTypeInfo().Assembly;
        MapsterSettings.Configure();
        return services
            .AddApiVersioning()
            .AddElsaWorkflow(config)
            .AddAuth(config)
            .AddBackgroundJobs(config)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddExceptionMiddleware()
            .AddBehaviours(applicationAssembly)
            .AddHealthCheck()
            .AddPOLocalization(config)
            .AddMailing(config)
            .AddMediatR(c => c.RegisterServicesFromAssemblyContaining(typeof(Startup)))
            // AddMediatR(c => c.RegisterServicesFromAssemblyContaining(typeof(Assembly.GetExecutingAssembly()))))
            .AddMultitenancy()
            .AddNotifications(config)
            .AddOpenApiDocumentation(config)
            .AddSEO(config)
            .AddPersistence()
            .AddRequestLogging(config)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddServices();
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        return services;
    }

    private static IServiceCollection AddHealthCheck(this IServiceCollection services) =>
        services.AddHealthChecks().AddCheck<TenantHealthCheck>("Tenant").Services;

    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabasesAsync(cancellationToken);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
    {
        builder
            .UseRequestLocalization()
            .UseStaticFiles()
            .UseSecurityHeaders(config)
            .UseFileStorage()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseCorsPolicy()
            .UseAuthentication()
            .UseCurrentUser()
            .UseMultiTenancy()
            .UseAuthorization()
            .UseRequestLogging(config)
            .UseHangfireDashboard(config)
            .UseElsaWorkflow(config)
            .UseOpenApiDocumentation(config)
            .UseDynamicDbContext();
        return builder;
    }
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization();
        builder.MapHealthCheck();
        builder.MapNotifications();
        return builder;
    }

    private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/api/health");
}