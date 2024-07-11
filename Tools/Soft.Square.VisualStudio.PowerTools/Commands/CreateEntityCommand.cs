namespace Soft.Square.VisualStudio.PowerTools;

using Microsoft;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Shell;
using Microsoft.VisualStudio.Extensibility.ToolWindows;
using Soft.Square.VisualStudio.PowerTools.ToolsWindows;
using System.Diagnostics;
using System.Globalization;

/// <summary>
/// CreateEntityCommand handler.
/// </summary>
[VisualStudioContribution]
internal class CreateEntityCommand : Command
{
    private readonly TraceSource logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEntityCommand"/> class.
    /// </summary>
    /// <param name="traceSource">Trace source instance to utilize.</param>
    public CreateEntityCommand(TraceSource traceSource)
    {
        // This optional TraceSource can be used for logging in the command. You can use dependency injection to access
        // other services here as well.
        WorkspacesExtensibility workSpace = this.Extensibility.Workspaces();
        this.logger = Requires.NotNull(traceSource, nameof(traceSource));
    }

    /// <inheritdoc />
    public override CommandConfiguration CommandConfiguration => new("%Soft.Square.VisualStudio.PowerTools.CreateEntityCommand.DisplayName%")
    {
        // Use this object initializer to set optional parameters for the command. The required parameter,
        // displayName, is set above. DisplayName is localized and references an entry in .vsextension\string-resources.json.
        // Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        // Placements = [CommandPlacement.KnownPlacements.ExtensionsMenu],
        // Placements = new[] { CommandPlacement.KnownPlacements.ExtensionsMenu },
        Placements = new[] { CommandPlacement.KnownPlacements.ViewOtherWindowsMenu },
        Icon = new(ImageMoniker.KnownValues.OfficeWebExtension, IconSettings.IconAndText),
       //  VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveEditorContentType, ".+"),

    };

    /// <inheritdoc />
    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        // Use InitializeAsync for any one-time setup or initialization.
        return base.InitializeAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        // await this.Extensibility.Shell().ShowPromptAsync("Hello from Create Entity extension Command!", PromptOptions.OK, cancellationToken);
        await this.Extensibility.Shell().ShowToolWindowAsync<CreateEntityToolWindow>(activate: true, cancellationToken);
    }
}
