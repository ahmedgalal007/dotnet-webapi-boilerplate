using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class TradeAction : Action
{
    public override string TypeName { get; protected set; } = nameof(TradeAction);
}

