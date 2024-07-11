using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.ToolWindows;
using Microsoft.VisualStudio.RpcContracts.RemoteUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Soft.Square.VisualStudio.PowerTools;

namespace Soft.Square.VisualStudio.PowerTools.ToolsWindows;
internal class CreateEntityToolWindow : ToolWindow
{
    private readonly EntityNameUserControl content = new();
    public CreateEntityToolWindow(VisualStudioExtensibility extensibility) : base(extensibility)
    {
        this.Title = "Create New Entity";
    }
    public override ToolWindowConfiguration ToolWindowConfiguration => new()
    {
        Placement = ToolWindowPlacement.DocumentWell,
    };


    public override Task InitializeAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public override async Task<IRemoteUserControl> GetContentAsync(CancellationToken cancellationToken)
        => content;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            content.Dispose();

        base.Dispose(disposing);
    }
}
