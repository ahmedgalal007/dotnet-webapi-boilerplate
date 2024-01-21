using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class AnatomicalStructure : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(AnatomicalStructure);
}
