using System;

namespace FSH.WebApi.Domain.Schemas.Things.Places;
public class Residence : Place
{
    public override string TypeName { get; protected set; } = nameof(Residence);
}
