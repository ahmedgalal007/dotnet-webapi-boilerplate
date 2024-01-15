using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Common.Contracts;
public abstract class MediaBaseEntity<T> : LocalizedEntity<T>
    where T : ILocalizableEntity, new()
{
    public string Url { get; set; }
    public string TypeName { get; set; }
    public Guid FileId { get; set; }
    public virtual IEnumerable<Keyword> Keywords { get; set; }
}
