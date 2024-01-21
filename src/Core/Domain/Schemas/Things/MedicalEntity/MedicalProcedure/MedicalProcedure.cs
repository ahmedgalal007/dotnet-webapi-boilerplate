using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class MedicalProcedure : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(MedicalProcedure);
}
