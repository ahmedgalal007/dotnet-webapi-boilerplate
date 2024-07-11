using Soft.Square.Modules.Shared.Abstraction;
using Soft.Square.Modules.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Soft.Square.Modules.Shared.Features;
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
