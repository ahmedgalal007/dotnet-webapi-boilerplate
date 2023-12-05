using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Article;
public class LocalizedNews : AuditableEntity, ILocalizableEntity, IAggregateRoot
{
    public string culturCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? SubTitle { get; set; }
    public string? SEOTitle { get; set; }
    public string? SocialTitle { get; set; }
    public string? Body { get; set; }
    public virtual News News { get; set; }
}
