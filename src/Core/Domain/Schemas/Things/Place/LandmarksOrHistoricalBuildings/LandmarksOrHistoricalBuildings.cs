using System;

namespace FSH.WebApi.Domain.Schemas.Things.Places;
public class LandmarksOrHistoricalBuildings : Place
{
    public override string TypeName { get; protected set; } = nameof(LandmarksOrHistoricalBuildings);
}
