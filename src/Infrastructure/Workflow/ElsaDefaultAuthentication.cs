using System;
using System.Linq;
using System.Security.Claims;
using Antlr4.Runtime.Misc;
using AspNetCore.Authentication.ApiKey;
using Elsa.Extensions;
using Elsa.Features.Abstractions;
using Elsa.Features.Attributes;
using Elsa.Features.Services;
using Elsa.Identity.Features;
using Elsa.Identity.Models;
using Elsa.Identity.Providers;
using Elsa.Requirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.WebApi.Infrastructure.Workflow;

//
// Summary:
//     Provides an authorization feature that configures the system with JWT bearer
//     and API key authentication.
[DependsOn(typeof(IdentityFeature))]
public class ElsaDefaultAuthentication : FeatureBase
{
    private const string MultiScheme = "Jwt-or-ApiKey";

    private Func<AuthenticationBuilder, AuthenticationBuilder> _configureApiKeyAuthorization = (AuthenticationBuilder builder) => builder.AddApiKeyInAuthorizationHeader<DefaultApiKeyProvider>();

    //
    // Summary:
    //     Gets or sets the Elsa.Identity.Features.DefaultAuthenticationFeature.ApiKeyProviderType.
    public Type ApiKeyProviderType { get; set; } = typeof(DefaultApiKeyProvider);


    public ElsaDefaultAuthentication(IModule module)
    : base(module)
    {
    }

    //
    // Summary:
    //     Configures the API key provider type.
    //
    // Type parameters:
    //   T:
    //     The type of the API key provider.
    //
    // Returns:
    //     The current Elsa.Identity.Features.DefaultAuthenticationFeature.
    public ElsaDefaultAuthentication UseApiKeyAuthorization<T>() where T : class, IApiKeyProvider
    {
        _configureApiKeyAuthorization = (AuthenticationBuilder builder) => builder.AddApiKeyInAuthorizationHeader<T>();
        return this;
    }

    //
    // Summary:
    //     Configures the API key provider type to Elsa.Identity.Providers.AdminApiKeyProvider.
    //
    //
    // Returns:
    //     The current Elsa.Identity.Features.DefaultAuthenticationFeature.
    public ElsaDefaultAuthentication UseAdminApiKey()
    {
        return UseApiKeyAuthorization<AdminApiKeyProvider>();
    }

    public override void Apply()
    {
        base.Services.ConfigureOptions<ConfigureJwtBearerOptions>();
        base.Services.ConfigureOptions<ValidateIdentityTokenOptions>();
        AuthenticationBuilder arg = base.Services.AddAuthentication("Jwt-or-ApiKey").AddPolicyScheme("Jwt-or-ApiKey", "Jwt-or-ApiKey", delegate (PolicySchemeOptions options)
        {
            options.ForwardDefaultSelector = (HttpContext context) => (!context.Request.Headers.Authorization.Any((string x) => x.Contains("ApiKey"))) ? "Bearer" : "ApiKey";
        });
        // .AddJwtBearer();
        _configureApiKeyAuthorization(arg);
        base.Services.AddScoped<IAuthorizationHandler, LocalHostRequirementHandler>();
        base.Services.AddScoped<IAuthorizationHandler, LocalHostPermissionRequirementHandler>();
        base.Services.AddScoped(ApiKeyProviderType);
        base.Services.AddScoped((IServiceProvider sp) => (IApiKeyProvider)sp.GetRequiredService(ApiKeyProviderType));
        base.Services.AddAuthorization(delegate (AuthorizationOptions options)
        {
            options.AddPolicy("SecurityRoot", delegate (AuthorizationPolicyBuilder policy)
            {
                policy.AddRequirements(new LocalHostPermissionRequirement());
            });
        });
    }
}

public static class ElsaDefaultAuthExtension{
    public static IModule UseElsaDefaultAuthentication(this IModule module, Action<ElsaDefaultAuthentication>? configure = null)
    {
        module.Configure(configure);

        return module;
    }

    private static Task<IApiKey> GetApiKey(string key)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("permissions", "*")
        };
        return Task.FromResult((IApiKey)new ApiKey(key, "admin", claims));
    }
}