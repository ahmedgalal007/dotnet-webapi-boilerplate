using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Keywords;
public class LocalizedKeyword : AuditableEntity, ILocalizableEntity
{
    public string culturCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
