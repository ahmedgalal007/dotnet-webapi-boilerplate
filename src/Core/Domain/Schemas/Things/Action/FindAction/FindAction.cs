using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class FindAction : Action
{
    public override string TypeName { get; protected set; } = nameof(FindAction);
}

