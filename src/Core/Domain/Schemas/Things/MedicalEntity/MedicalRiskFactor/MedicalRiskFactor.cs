using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class MedicalRiskFactor : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(MedicalRiskFactor);
}
