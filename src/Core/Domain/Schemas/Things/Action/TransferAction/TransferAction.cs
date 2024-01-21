using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class TransferAction : Action
{
    public override string TypeName { get; protected set; } = nameof(TransferAction);
}

