using System;

namespace FSH.WebApi.Domain.Schemas.Things;
public abstract class MedicalEntity : Thing
{
    public override string TypeName { get; protected set; } = nameof(MedicalEntity);
}


// TODO AnatomicalStructure
// TODO AnatomicalSystem
// TODO DrugClass
// TODO DrugCost
// TODO LifestyleModification
// TODO MedicalCause
// TODO MedicalCondition
// TODO MedicalContraindication
// TODO MedicalDevice
// TODO MedicalGuideline
// TODO MedicalIndication
// TODO MedicalIntangible
// TODO MedicalProcedure
// TODO MedicalRiskEstimator
// TODO MedicalRiskFactor
// TODO MedicalStudy
// TODO MedicalTest
// TODO Substance
// TODO SuperficialAnatomy