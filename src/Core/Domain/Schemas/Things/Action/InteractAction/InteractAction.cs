using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class InteractAction : Action
{
    public override string TypeName { get; protected set; } = nameof(InteractAction);
}

