﻿using FSH.WebApi.Domain.Schemas.Things.BioChemEntity.BioChemEntities;
using System;

namespace FSH.WebApi.Domain.Schemas.Things.BioChemEntity;
public abstract class BioChemEntity : Thing
{
    public override string TypeName { get; protected set; } = nameof(BioChemEntity);
}

// TODO ChemicalSubstance
// TODO Gene
// TODO MolecularEntity
// TODO Protein