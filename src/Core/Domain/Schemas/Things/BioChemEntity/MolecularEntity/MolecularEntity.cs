using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.BioChemEntities;
public class MolecularEntity : BioChemEntity
{
    public override string TypeName { get; protected set; } = nameof(MolecularEntity);
}
