using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class MedicalRiskEstimator : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(MedicalRiskEstimator);
}
