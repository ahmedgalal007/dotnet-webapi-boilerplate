using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class UpdateAction : Action
{
    public override string TypeName { get; protected set; } = nameof(UpdateAction);
}

