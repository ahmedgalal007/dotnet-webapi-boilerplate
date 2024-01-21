using System;

namespace FSH.WebApi.Domain.Schemas.Things.Places;
public class LocalBusiness : Place
{
    public override string TypeName { get; protected set; } = nameof(LocalBusiness);
}
