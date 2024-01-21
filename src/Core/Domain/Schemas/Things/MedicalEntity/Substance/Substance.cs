using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class Substance : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(Substance);
}
