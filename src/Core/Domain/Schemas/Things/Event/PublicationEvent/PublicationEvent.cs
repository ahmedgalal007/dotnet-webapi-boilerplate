﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Events;
public class PublicationEvent : Event
{
    public override string TypeName { get; protected set; } = nameof(PublicationEvent);
}
