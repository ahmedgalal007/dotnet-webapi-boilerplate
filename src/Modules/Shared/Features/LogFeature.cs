using FSH.WebApi.Modules.Shared.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Modules.Shared.Features;
public class LogFeature : FeatureBase
{
    public LogFeature(IModule module) : base(module)
    {
    }

    /// <summary>
    /// A delegate to configure <see cref="LoggingOptions"/>.
    /// </summary>
    public Action<LoggingOptions>? ConfigureHttpOptions { get; set; }
    /// <summary>
    /// A delegate that is invoked when authorizing an inbound HTTP request.
    /// </summary>
    // public Func<IServiceProvider, ILoggingEndpointAuthorizationHandler> LoggingAuthorizationHandler { get; set; } = sp => sp.GetRequiredService<AuthenticationBasedHttpEndpointAuthorizationHandler>();

    /// <summary>
    /// A delegate that is invoked when an HTTP workflow faults.
    /// </summary>
    // public Func<IServiceProvider, ILoggingEndpointFaultHandler> LoggingEndpointFaultHandler { get; set; } = sp => sp.GetRequiredService<DefaultHttpEndpointFaultHandler>();

    public override void Apply()
    {
        base.Apply();

        var configureOptions = ConfigureHttpOptions ?? (options =>
        {
            options.BasePath = "/workflows";
            options.BaseUrl = new Uri("http://localhost");
        });

    }
}
