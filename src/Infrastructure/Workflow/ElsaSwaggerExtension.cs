#region Assembly Elsa.Api.Common, Version=3.1.3.0, Culture=neutral, PublicKeyToken=null
// C:\Users\ahmed\.nuget\packages\elsa.api.common\3.1.3\lib\net7.0\Elsa.Api.Common.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion
using System;
using Elsa.Features.Services;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using NSwag;
using NSwag.Generation.AspNetCore;

// namespace Elsa.Extensions;
namespace FSH.WebApi.Infrastructure.Workflow;

// Summary:
//     Extensions to enable the generation of Swagger API Documentation and associated
//     Swagger UI.
public static class SwaggerExtensions
{
    //
    // Summary:
    //     Registers Swagger document generator.
    public static IModule AddOpenApi(this IModule module)
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
                    Description = "Enter: ApiKey [your API key]"
                });
            };
        });
        return module;
    }

    //
    // Summary:
    //     Adds middleware to enable the Swagger UI at '/swagger'
    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
    {
        return app.UseSwaggerGen();
    }
}
