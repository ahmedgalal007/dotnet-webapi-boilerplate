using FSH.WebApi.Domain.Schemas.Things.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Actions;
public class SeekToAction : Action
{
    public override string TypeName { get; protected set; } = nameof(SeekToAction);
}

