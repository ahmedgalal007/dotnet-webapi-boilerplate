using System;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class SearchAction : Action
{
    public override string TypeName { get; protected set; } = nameof(SearchAction);
}

