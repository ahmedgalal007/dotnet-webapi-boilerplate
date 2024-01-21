using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class PlayAction : Action
{
    public override string TypeName { get; protected set; } = nameof(PlayAction);
}

