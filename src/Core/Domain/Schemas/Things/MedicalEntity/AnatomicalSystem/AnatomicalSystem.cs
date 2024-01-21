using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class AnatomicalSystem : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(AnatomicalSystem);
}
