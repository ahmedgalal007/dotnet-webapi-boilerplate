using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class ConsumeAction : Action
{
    public override string TypeName { get; protected set; } = nameof(ConsumeAction);
}

