using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.BioChemEntity.BioChemEntities;
public class ChemicalSubstance : BioChemEntity
{
    public override String TypeName { get; protected set; } = nameof(ChemicalSubstance);
}
