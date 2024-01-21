using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Events;
public class MusicEvent : Event
{
    public override string TypeName { get; protected set; } = nameof(MusicEvent);
}
