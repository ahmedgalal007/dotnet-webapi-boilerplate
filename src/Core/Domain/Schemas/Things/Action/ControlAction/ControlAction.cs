using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class ControlAction : Action
{
    public override string TypeName { get; protected set; } = nameof(ControlAction);
}

