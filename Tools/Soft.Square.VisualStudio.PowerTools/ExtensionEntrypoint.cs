namespace Soft.Square.VisualStudio.PowerTools;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

/// <summary>
/// Extension entrypoint for the VisualStudio.Extensibility extension.
/// </summary>
[VisualStudioContribution]
internal class ExtensionEntrypoint : Extension
{
    /// <inheritdoc/>
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
                id: "Soft.Square.VisualStudio.PowerTools.2d7bd755-f176-489b-a3a8-e08d507612db",
                version: this.ExtensionAssemblyVersion,
                publisherName: "Publisher name",
                displayName: "Soft.Square.VisualStudio.PowerTools",
                description: "Extension description"),
    };

    /// <inheritdoc />
    protected override void InitializeServices(IServiceCollection serviceCollection)
    {
        base.InitializeServices(serviceCollection);

        // You can configure dependency injection here by adding services to the serviceCollection.
    }

    [VisualStudioContribution]
    public static MenuConfiguration EntitiesChildMenu => new("%Soft.Square.VisualStudio.PowerTools.EntitiesChildMenu.DisplayName%")
    {
        Children = new[]
        {
            MenuChild.Group(GroupChild.Command<InsertGuidCommand>()),
            MenuChild.Group(GroupChild.Command<CreateEntityCommand>()),
        }
    };

    [VisualStudioContribution]
    public static MenuConfiguration SoftSquareMenu => new("%Soft.Square.VisualStudio.PowerTools.SoftSquareMenu.DisplayName%")
    {
        Placements = new CommandPlacement[] {
             CommandPlacement.KnownPlacements.ToolsMenu,
        },
        Children = new[]
        {
            MenuChild.Menu(EntitiesChildMenu),
        }
    };
}
