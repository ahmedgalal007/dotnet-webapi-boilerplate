using Asp.Versioning;
using AspNetCore.Authentication.ApiKey;
using Elsa.EntityFrameworkCore.Common;
using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.EntityFrameworkCore.Modules.Identity;
using Elsa.Http.Features;
using Elsa.Workflows;
using Elsa.WorkflowProviders.BlobStorage;
using Elsa.Workflows.Management.Compression;
using Elsa.Workflows.Management.Options;
using Elsa.Features.Services;
using Elsa.Extensions;
using Elsa.Identity.Models;
using Esprima.Ast;
using FSH.WebApi.Infrastructure.Auth;
using FSH.WebApi.Infrastructure.Auth.Jwt;
using FSH.WebApi.Infrastructure.BackgroundJobs;
using FSH.WebApi.Infrastructure.Common;
using FSH.WebApi.Infrastructure.Workflow.Workflows;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Elsa.Workflows.Management.Compression;
using Elsa.Workflows.Management.Services;
using System.Configuration;
using System.Text.Encodings.Web;
using NSwag.Generation.AspNetCore;
using NSwag;
using Elsa.Http.Options;
using Microsoft.Extensions.Options;
using FastEndpoints.Swagger;
using NSwag.Generation.Processors.Security;
using FSH.WebApi.Infrastructure.OpenApi;
using NJsonSchema.Generation.TypeMappers;
using Org.BouncyCastle.Tls;
using ZymLabs.NSwag.FluentValidation;
using Microsoft.Extensions.Logging;
using FSH.WebApi.Application.Common.Localization;
using System;
using Jint;

namespace FSH.WebApi.Infrastructure.Workflow;
internal static class Startup
{
    private static readonly Serilog.ILogger _logger = Log.ForContext(typeof(Startup));
    const bool runEFCoreMigrations = true;
    internal static IServiceCollection AddElsaWorkflow(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors(cors => cors.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*")));


        var storageSettings = config.GetSection("ElsaSettings:Storage").Get<ElsaStorageSettings>();
        if (storageSettings is null) throw new Exception("Elsa Storage Provider is not configured.");
        if (string.IsNullOrEmpty(storageSettings.StorageProvider)) throw new Exception("Elsa Storage Provider is not configured.");
        if (string.IsNullOrEmpty(storageSettings.ConnectionString)) throw new Exception("Elsa Storage Provider ConnectionString is not configured.");
        _logger.Information($"Elsa: Current Storage Provider : {storageSettings.StorageProvider}");
        _logger.Information("For more Elsa storage, visit https://aspnetzero.com/blog/integrating-elsa-with-aspnet-zero-angular");

        services.AddEndpointsApiExplorer();
        services.AddElsa(elsa =>
        {
            elsa
            .AddActivitiesFrom<HttpHelloWorld>()
            .AddWorkflowsFrom<HttpHelloWorld>()
            .UseFluentStorageProvider()
            .UseFileStorage()
            .UseIdentity(identity =>
            {
                identity.UseEntityFrameworkCore(ef =>
                {
                    ef.UseDatabase(storageSettings.StorageProvider, storageSettings.ConnectionString, config);

                    ef.RunMigrations = runEFCoreMigrations;
                });
                var identitySection = config.GetSection("ElsaSettings:Identity");
                var identityTokenSection = identitySection.GetSection("Tokens");
                identity.IdentityOptions = options => identitySection.Bind(options);
                identity.TokenOptions = options => identityTokenSection.Bind(options);
                identity.UseConfigurationBasedUserProvider(options => identitySection.Bind(options));
                identity.UseConfigurationBasedApplicationProvider(options => identitySection.Bind(options));
                identity.UseConfigurationBasedRoleProvider(options => identitySection.Bind(options));
            })
            // .UseIdentity("48587230567A646D394B435A6277734A-4802fa49-e91e-45e8-b00f-b5492377e20b")
            .UseElsaDefaultAuthentication();
            // Configure Management layer to use EF Core.
            elsa.UseWorkflowManagement(
                management =>
                {
                    management
                       .UseEntityFrameworkCore(o => o.UseElsaManagementDatabase(storageSettings.StorageProvider, storageSettings.ConnectionString, config));
                    // if (useZipCompression)
                    management.SetCompressionAlgorithm(nameof(Zstd));

                    // if (useMemoryStores)
                    management.UseWorkflowInstances(feature => feature.WorkflowInstanceStore = sp => sp.GetRequiredService<MemoryWorkflowInstanceStore>());

                    // if (useMassTransit)
                    //    management.UseMassTransitDispatcher();

                    // if (useCaching)
                    //    management.UseCache();

                    // management.SetDefaultLogPersistenceMode(LogPersistenceMode.Inherit);
                    // management.UseReadOnlyMode(false);
                });
            elsa.UseScheduling(scheduling =>
             {
                 //if (useHangfire)
                 //    scheduling.UseHangfireScheduler();

                 //if (useQuartz)
                 //    scheduling.UseQuartzScheduler();
             });
            elsa.UseWorkflowsApi(api =>
             {
                 api.AddFastEndpointsAssembly<HttpHelloWorld>();
             });
            // elsa.UseRealTimeWorkflows();
            // Configure Runtime layer to use EF Core.
            /*elsa.UseWorkflowRuntime(runtime => runtime.UseEntityFrameworkCore(R => R.UsePersistenceDb(storageSettings.StorageProvider, storageSettings.ConnectionString, config)));*/
            elsa.UseCSharp(options =>
             {
                 options.AppendScript("string Greet(string name) => $\"Hello {name}!\";");
                 options.AppendScript("string SayHelloWorld() => Greet(\"World\");");
             })
            .UseJavaScript(options =>
            {
                options.AllowClrAccess = true;
                options.ConfigureEngine(engine =>
                {
                    engine.Execute("function greet(name) { return `Hello ${name}!`; }");
                    engine.Execute("function sayHelloWorld() { return greet('World'); }");
                });
            });
            elsa.UsePython(python =>
            {
                python.PythonOptions += options =>
                {
                    // Make sure to configure the path to the python DLL. E.g. /opt/homebrew/Cellar/python@3.11/3.11.6_1/Frameworks/Python.framework/Versions/3.11/bin/python3.11
                    // alternatively, you can set the PYTHONNET_PYDLL environment variable.
                    config.GetSection("ElsaSettings:Scripting:Python").Bind(options);
                };
            })
            .UseLiquid(liquid => liquid.FluidOptions = options => options.Encoder = HtmlEncoder.Default);
            /*elsa.UseHttp(http =>
            {
                http.ConfigureHttpOptions = options => config.GetSection("ElsaSettings:Http").Bind(options);

                // if (useCaching)
                //    http.UseCache();
            });
            elsa.UseEmail(email => email.ConfigureOptions = options => config.GetSection("ElsaSettings:Smtp").Bind(options));*/

            //var settings = config.GetSection($"{nameof(SecuritySettings)}:{nameof(JwtSettings)}").Get<JwtSettings>();
            //elsa.UseIdentity(identity =>
            //{

            //    identity.TokenOptions = options => options.SigningKey = settings.Key; // This key needs to be at least 256 bits long.
            //    // identity.TokenOptions = options => options.SigningKey = ""; // This key needs to be at least 256 bits long.
            //    identity.UseAdminUserProvider();
            //});

            // Configure ASP.NET authentication/ authorization.
            // elsa.UseDefaultAuthentication(auth => auth.UseAdminApiKey());

            // elsa.UseDefaultAuthentication();

            // elsa.UseElsaDefaultAuthentication(auth => auth.UseAdminApiKey());
            // elsa.UseElsaDefaultAuthentication();

            // elsa.UseLiquid();

            // Setup a SignalR hub for real-time updates from the server.



            // Add FastEndpoints.
            //elsa.UseWorkflowsApi()

            // Enable SAS tokens.
            //.UseSasTokens()
            //.UseRealTimeWorkflows()
            //.UseWorkflowsApi()
            //.UseScheduling();

            //elsa.UseHttp(http => http.ConfigureHttpOptions = options =>
            //{
            //    options.BaseUrl = new Uri("https://localhost:5001");
            //    options.BasePath = "/workflows";
            //});
            //elsa.AddWorkflow<HttpHelloWorld>();

            // elsa.AddOpenApi();
            ;
            // Register custom activities from the application, if any.

            elsa.UseWorkflowContexts();

            elsa.AddOpenApi();
            // elsa.AddSwagger();
            // elsa.AddElsaOpenApi(config);
            elsa.AddFastEndpointsAssembly<HttpHelloWorld>();
        });
        
        //services.AddSwaggerGen(option =>
        //{
        //    // issue for Elsa fix
        //    option.CustomSchemaIds(type => type.ToString());

        //    // ¹w³]¨Ï¥ÎJWT token
        //    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //    {
        //        In = ParameterLocation.Header,
        //        Description = "Enter: ApiKey [your API key]",
        //        Name = "Authorization",
        //        Type = SecuritySchemeType.ApiKey,
        //        BearerFormat = "JWT",
        //        Scheme = "Bearer"
        //    });
        //    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        {
        //            new OpenApiSecurityScheme
        //            {
        //                Reference = new OpenApiReference
        //                {
        //                    Type = ReferenceType.SecurityScheme,
        //                    Id = "Bearer"
        //                }
        //            },
        //            new string[] { }
        //        }
        //    });
        //});
        return services;
    }

    internal static IApplicationBuilder UseElsaWorkflow(this IApplicationBuilder app, IConfiguration config)
    {
        // app.UseCors(CorsPolicy);
        // Elsa API endpoints for designer.
        var routePrefix = app.ApplicationServices.GetRequiredService<IOptions<HttpActivityOptions>>().Value.ApiRoutePrefix;
        app.UseWorkflowsApi(routePrefix);
        // Captures unhandled exceptions and returns a JSON response.
        app.UseJsonSerializationErrorHandler();

        // Elsa HTTP Endpoint activities.
        app.UseWorkflows();

        // Swagger API documentation.
        // app.UseOpenApi();
        return app; // .UseSwaggerUI();
        //return app.UseWorkflowsApi() // Use Elsa API endpoints
        //            .UseWorkflows() // Use Elsa middleware to handle HTTP requests mapped to HTTP Endpoint activities
        //            .UseWorkflowsSignalRHubs(); // Optional SignalR integration. Elsa Studio uses SignalR to receive real-time updates from the server
    }

    public static EFCoreWorkflowRuntimePersistenceFeature UsePersistenceDb(this EFCoreWorkflowRuntimePersistenceFeature feature, string dbProvider, string connectionString, IConfiguration config) =>
        dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql =>
                feature.UsePostgreSql(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.SqlServer =>
                feature.UseSqlServer(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.SqLite =>
                feature.UseSqlite(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.MySql =>
                feature.UseMySql(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            _ => throw new Exception($"Elsa Storage Provider {dbProvider} is not supported.")
        };
    public static WorkflowManagementPersistenceFeature UseElsaManagementDatabase(this WorkflowManagementPersistenceFeature feature, string dbProvider, string connectionString, IConfiguration config) =>

        // , config.GetSection("ElsaSettings:Storage:Options").Get<PostgreSqlStorageOptions>()

        dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql =>
                feature.UsePostgreSql(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.SqlServer =>
                feature.UseSqlServer(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.SqLite =>
                feature.UseSqlite(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.MySql =>
                feature.UseMySql(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            _ => throw new Exception($"Elsa Storage Provider {dbProvider} is not supported.")
        };
    public static EFCoreIdentityPersistenceFeature UseDatabase(this EFCoreIdentityPersistenceFeature feature, string dbProvider, string connectionString, IConfiguration config) =>

        // , config.GetSection("ElsaSettings:Storage:Options").Get<PostgreSqlStorageOptions>()

        dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql =>
                feature.UsePostgreSql(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.SqlServer =>
                feature.UseSqlServer(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.SqLite =>
                feature.UseSqlite(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            DbProviderKeys.MySql =>
                feature.UseMySql(connectionString, config.GetSection("ElsaSettings:Storage:Options").Get<ElsaDbContextOptions>()),
            _ => throw new Exception($"Elsa Storage Provider {dbProvider} is not supported.")
        };

    public static IModule AddElsaOpenApi(this IModule module, IConfiguration config)
    {
        Version ver = new Version(3, 0);
        module.Services.SwaggerDocument(delegate (DocumentOptions o)
        {
            o.EnableJWTBearerAuth = true;
            o.DocumentSettings = delegate (AspNetCoreOpenApiDocumentGeneratorSettings s)
            {
                s.DocumentName = $"v{ver.Major}";
                s.Title = "Elsa API";
                s.Version = $"v{ver.Major}.{ver.Minor}";
                s.AddAuth("ApiKey", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Scheme = ApiKeyDefaults.AuthenticationScheme,
                    BearerFormat = "Authorization: ApiKey xxxxxxx-xxxxxxx-xxxxxxx",
                    Description = "Enter: ApiKey [your API key]"
                });
                s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TimeSpan), schema =>
                {
                    schema.Type = NJsonSchema.JsonObjectType.String;
                    schema.IsNullableRaw = true;
                    schema.Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$";
                    schema.Example = "02:00:00";
                }));
            };
        });
        return module;
    }

}
