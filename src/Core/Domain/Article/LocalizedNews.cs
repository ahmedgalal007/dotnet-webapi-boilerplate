using FSH.WebApi.Domain.Common.Localizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Article;
public class LocalizedNews : AuditableEntity, ILocalizableEntity
{
    [MaxLength(6)]
    public string culturCode { get; set; } = string.Empty;
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(60)]
    public string? SubTitle { get; set; }
    [MaxLength(60)]
    public string? SEOTitle { get; set; }
    [MaxLength(60)]
    public string? SocialTitle { get; set; }
    [MaxLength(4000)]
    public string? Body { get; set; }
    public virtual News News { get; set; }
}
