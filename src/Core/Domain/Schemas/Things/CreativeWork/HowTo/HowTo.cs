using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.CreativeWork;
public class HowTo : CreativeWork
{
    public override string TypeName { get; protected set; } = nameof(HowTo);
}
