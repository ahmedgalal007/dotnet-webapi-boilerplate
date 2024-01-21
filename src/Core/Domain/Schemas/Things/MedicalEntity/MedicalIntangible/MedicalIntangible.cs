using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class MedicalIntangible : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(MedicalIntangible);
}
