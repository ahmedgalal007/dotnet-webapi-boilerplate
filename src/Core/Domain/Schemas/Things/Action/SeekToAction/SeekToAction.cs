using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class SeekToAction : Action
{
    public override string TypeName { get; protected set; } = nameof(SeekToAction);
}

