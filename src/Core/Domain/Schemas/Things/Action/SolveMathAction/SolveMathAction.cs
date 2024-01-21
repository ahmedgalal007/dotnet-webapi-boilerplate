using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class SolveMathAction : Action
{
    public override string TypeName { get; protected set; } = nameof(SolveMathAction);
}

