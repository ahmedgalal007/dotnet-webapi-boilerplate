using System.Net.Http;

namespace Soft.Square.Modules.Shared.Contracts;

public record AuthorizeLoggingEndpointContext(/*HttpContext HttpContext, Workflow Workflow,*/ string? Policy = default);
public interface ILoggingEndpointAuthorizationHandler
{
    /// <summary>
    /// Authorizes an inbound HTTP request.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>True if the request is authorized, otherwise false.</returns>
    ValueTask<bool> AuthorizeAsync(AuthorizeLoggingEndpointContext context);
}