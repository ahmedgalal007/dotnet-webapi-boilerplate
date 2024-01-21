using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class MedicalStudy : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(MedicalStudy);
}
