using System;
using FSH.WebApi.Domain.Keywords;

namespace FSH.WebApi.Domain.Medias;
public class Media : MediaBaseEntity<LocalizedMedia>, IAggregateRoot
{
    public virtual IEnumerable<Keyword>? Keywords { get; set; }
}
