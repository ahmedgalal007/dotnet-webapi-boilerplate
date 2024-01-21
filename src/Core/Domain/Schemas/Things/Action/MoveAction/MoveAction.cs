using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class MoveAction : Action
{
    public override string TypeName { get; protected set; } = nameof(MoveAction);
}

