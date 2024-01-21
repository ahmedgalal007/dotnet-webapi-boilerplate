using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Organization.Organizations;
public class LibrarySystem : Organization
{
    public override string TypeName { get; protected set; } = nameof(LibrarySystem);
}
