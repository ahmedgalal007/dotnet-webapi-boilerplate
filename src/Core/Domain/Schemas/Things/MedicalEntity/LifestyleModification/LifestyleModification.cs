using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class LifestyleModification : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(LifestyleModification);
}
