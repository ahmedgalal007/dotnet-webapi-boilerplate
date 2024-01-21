using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class MedicalCondition : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(MedicalCondition);
}
