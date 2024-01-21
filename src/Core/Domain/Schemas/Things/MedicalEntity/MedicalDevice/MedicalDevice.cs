using System;

namespace FSH.WebApi.Domain.Schemas.Things.MedicalEntities;
public class MedicalDevice : MedicalEntity
{
    public override string TypeName { get; protected set; } = nameof(MedicalDevice);
}
