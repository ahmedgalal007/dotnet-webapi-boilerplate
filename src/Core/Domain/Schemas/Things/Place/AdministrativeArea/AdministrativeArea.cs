using System;

namespace FSH.WebApi.Domain.Schemas.Things.Places;
public class AdministrativeArea : Place
{
    public override string TypeName { get; protected set; } = nameof(AdministrativeArea);
}
