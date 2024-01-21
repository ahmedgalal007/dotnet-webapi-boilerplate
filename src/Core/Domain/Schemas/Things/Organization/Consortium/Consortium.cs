using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Organizations;
public class Consortium : Organization
{
    public override string TypeName { get; protected set; } = nameof(Consortium);
}
