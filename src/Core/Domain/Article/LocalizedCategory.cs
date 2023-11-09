using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Article;
public class LocalizedCategory : AuditableEntity, ILocalizableEntity
{
    [MaxLength(6)]
    public string culturCode { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;
    public virtual Category Category { get; set; }
}
