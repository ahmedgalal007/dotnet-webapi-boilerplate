using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Events;
public class BusinessEvent : Event
{
    public override string TypeName { get; protected set; } = nameof(BusinessEvent);
}
