using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Organization.Organizations;
public class Airline : Organization
{
    public override String TypeName { get; protected set; } = nameof(Airline);
}
