using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Intangibles;
public class Property : Intangible
{
    public override string Property { get; protected set; } = nameof(zzz);
}
