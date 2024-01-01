using System;
using System.Reflection;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Application.Common.SEO;
using FSH.WebApi.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using Serilog;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.SqlServer;
using FSH.WebApi.Infrastructure.Persistence.Configuration.CustomConfigurations;
using FSH.WebApi.Infrastructure.Persistence.Context;

namespace FSH.WebApi.Infrastructure.SEO;

internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddSEO(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<SEOSettings>()
            .BindConfiguration(nameof(SEOSettings))
            .PostConfigure(seoSettings =>
            {
                if (seoSettings.LoggerEnabled ?? false)
                {
                    var srv = new SeoUtilitiesService(config, _logger);

                    const string toSlugTitle = @" الرئيسية  أفريقيا\r\nحصاد ٢٠٢٣| هل يشهد العام الجديد وداع «أفريقيا الفرنسية»؟";

                    // const string toSlugTitle = @"Où trouver Campus France près de chez vous ?";
                    // string slugged = Regex.Replace(toSlugTitle, @"/[^a-z0-9_ءاأإآؤئبتثجحخدذرزسشصضطظعغفقكلمنهويةى\s-]/u", "-");

                    string slugged = srv.Slugify(toSlugTitle);

                    _logger.Information("Title: {myTitle}", toSlugTitle);
                    _logger.Information("Slugyfied: {slug}", slugged);
                }

                // services.AddScoped<ISeoUtilitiesService, SeoUtilitiesService>(provider => new SeoUtilitiesService(seoSettings, _logger));
            })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services
            .AddScoped<ISeoUtilitiesService, SeoUtilitiesService>(_ => new SeoUtilitiesService(config, _logger))
            .AddConfiguredEntityTypes(config!);
    }

    private static IServiceCollection AddConfiguredEntityTypes(this IServiceCollection services, IConfiguration config)
    {
        IEnumerable<Type> entitiesConf = typeof(ApplicationDbContext).Assembly.DefinedTypes
            .Where(t => !t.IsAbstract
                        && !t.IsGenericTypeDefinition
                        && typeof(EntityTypeConfigurationDependency).IsAssignableFrom(t));
        Assembly? assembly = Assembly.GetAssembly(typeof(Startup));
        var applyEntityConfigurationMethod = typeof(ModelBuilder)
            .GetMethods()
            .Single(
                e => e.Name == nameof(ModelBuilder.ApplyConfiguration)
                    && e.ContainsGenericParameters
                    && e.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition()
                    == typeof(IEntityTypeConfiguration<>));

        services.AddSingleton<Action<ModelBuilder>>(modelBuilder =>
        {
            foreach (var type in entitiesConf)
            {
                // ?modelBuilder.ApplyConfiguration(new TableBatchMasterConfiguration());

                _logger.Information("Applying Entity Configuration Method {entity} ", type.Name);
                _logger.Information("Generic Entity Type {entity} ", type.BaseType.GenericTypeArguments[0]);
                var method = applyEntityConfigurationMethod.MakeGenericMethod(type.BaseType.GenericTypeArguments[0]);
                _logger.Information("Generic Method {entity} ", method.Name);
                object? configClass = Activator.CreateInstance(type, new[] { config });
                _logger.Information("Constructors Parameter {0} ", configClass.GetType().GetConstructors().Last().GetParameters()[0].ParameterType.Name);
                method.Invoke(modelBuilder, new[] { configClass });
            }
        });

        return services;
    }
}
