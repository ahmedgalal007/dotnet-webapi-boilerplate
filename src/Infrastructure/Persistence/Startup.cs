using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Infrastructure.Common;
using FSH.WebApi.Infrastructure.Persistence.ConnectionString;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using FSH.WebApi.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Serilog;
using System.Configuration;

namespace FSH.WebApi.Infrastructure.Persistence;

internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .PostConfigure(databaseSettings =>
            {
                _logger.Information("Current DB Provider: {dbProvider}", databaseSettings.DBProvider);
            })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services
            .AddDbContext<ApplicationDbContext>((p, m) =>
            {
                var databaseSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                m.UseDatabase(databaseSettings.DBProvider, databaseSettings.ConnectionString);
            })
            .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<ApplicationDbInitializer>()
            .AddTransient<ApplicationDbSeeder>()
            .AddServices(typeof(ICustomSeeder), ServiceLifetime.Transient)
            .AddTransient<CustomSeederRunner>()

            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>()
            .AddTransient<IConnectionStringValidator, ConnectionStringValidator>()

            .AddRepositories()

            .AddDbContext<DynamicDbContext>((p, m) =>
            {
                var databaseSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;

                // m.UseDatabase(databaseSettings.DBProvider, databaseSettings.ConnectionString);
                // .ReplaceService<IModelCacheKeyFactory, DynamicDbCacheKeyFactory>()
                // .ReplaceService<IMigrationsAssembly, DbSchemaAwareMigrationAssembly>();
                var config = p.GetService<IConfiguration>();
                m.UseSqlServer(config.GetConnectionString("DynamicSchema"))
                .ReplaceService<IModelCacheKeyFactory, DynamicDbCacheKeyFactory>();

                // switch (databaseSettings.DBProvider.ToLowerInvariant())
                // {
                //     case DbProviderKeys.Npgsql:
                //         m.UseNpgsql(databaseSettings.ConnectionString);
                //         break;
                //     case DbProviderKeys.SqlServer:
                //         m.UseSqlServer(databaseSettings.ConnectionString);
                //         break;
                //     case DbProviderKeys.MySql:
                //         m.UseMySql(
                //             databaseSettings.ConnectionString,
                //             ServerVersion.AutoDetect(databaseSettings.ConnectionString),
                //             e => e.SchemaBehavior(MySqlSchemaBehavior.Ignore));
                //         break;
                //     case DbProviderKeys.Oracle:
                //         m.UseOracle(databaseSettings.ConnectionString);
                //         break;
                //     case DbProviderKeys.SqLite:
                //         m.UseSqlite(databaseSettings.ConnectionString);
                //         break;
                   
                //     default:
                //         throw new InvalidOperationException($"DB Provider {databaseSettings.DBProvider} is not supported.");
                // }
            });
    }

    internal static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
    {
        return dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql => builder.UseNpgsql(connectionString, e =>
                                 e.MigrationsAssembly("Migrators.PostgreSQL")),
            DbProviderKeys.SqlServer => builder.UseSqlServer(connectionString, e =>
                                 e.MigrationsAssembly("Migrators.MSSQL")),
            DbProviderKeys.MySql => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), e =>
                                 e.MigrationsAssembly("Migrators.MySQL")
                                  .SchemaBehavior(MySqlSchemaBehavior.Ignore)),
            DbProviderKeys.Oracle => builder.UseOracle(connectionString, e =>
                                 e.MigrationsAssembly("Migrators.Oracle")),
            DbProviderKeys.SqLite => builder.UseSqlite(connectionString, e =>
                                 e.MigrationsAssembly("Migrators.SqLite")),
            _ => throw new InvalidOperationException($"DB Provider {dbProvider} is not supported."),
        };
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Add Repositories
        services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

        foreach (var aggregateRootType in
            typeof(IAggregateRoot).Assembly.GetExportedTypes()
                .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                .ToList())
        {
            // Add ReadRepositories.
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));

            // Decorate the repositories with EventAddingRepositoryDecorators and expose them as IRepositoryWithEvents.
            services.AddScoped(typeof(IRepositoryWithEvents<>).MakeGenericType(aggregateRootType), sp =>
                Activator.CreateInstance(
                    typeof(EventAddingRepositoryDecorator<>).MakeGenericType(aggregateRootType),
                    sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)))
                ?? throw new InvalidOperationException($"Couldn't create EventAddingRepositoryDecorator for aggregateRootType {aggregateRootType.Name}"));
        }

        return services;
    }

    private static IServiceCollection AddDynamicRepositories(this IServiceCollection services)
    {
        // Add Repositories
        services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

        foreach (var aggregateRootType in
            typeof(IAggregateRoot).Assembly.GetExportedTypes()
                .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                .ToList())
        {
            // Add ReadRepositories.
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));

            // Decorate the repositories with EventAddingRepositoryDecorators and expose them as IRepositoryWithEvents.
            services.AddScoped(typeof(IRepositoryWithEvents<>).MakeGenericType(aggregateRootType), sp =>
                Activator.CreateInstance(
                    typeof(EventAddingRepositoryDecorator<>).MakeGenericType(aggregateRootType),
                    sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)))
                ?? throw new InvalidOperationException($"Couldn't create EventAddingRepositoryDecorator for aggregateRootType {aggregateRootType.Name}"));
        }

        return services;
    }
}