using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Article;
public class LocalizedCategory : AuditableEntity//, ILocalizableEntity
{
    public string culturCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    // public virtual Category Category { get; set; }
}
