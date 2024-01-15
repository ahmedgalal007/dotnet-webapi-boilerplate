using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Medias;
public class LocalizedMedia : AuditableEntity, ILocalizableEntity
{
    public string culturCode { get; set; } = string.Empty;
    public string TypeName { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DefaultIdType MediaId { get; set; }
}
