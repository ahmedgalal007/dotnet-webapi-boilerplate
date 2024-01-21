using System;

namespace FSH.WebApi.Domain.Schemas.Things.Places;
public class TouristDestination : Place
{
    public override string TypeName { get; protected set; } = nameof(TouristDestination);
}
