using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class DrugClass : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(DrugClass);
}
