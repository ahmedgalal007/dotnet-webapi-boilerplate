using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class AssessAction : Action
{
    public override string TypeName { get; protected set; } = nameof(AssessAction);
}

