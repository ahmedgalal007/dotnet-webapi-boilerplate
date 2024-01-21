using System;

namespace FSH.WebApi.Domain.Schemas.Things.Places;
public class Landform : Place
{
    public override string TypeName { get; protected set; } = nameof(Landform);
}
