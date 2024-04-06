using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Common.Localization;
public class LocalizedRequest<TID, TLocalizedEntity> : IRequest<TID>
{
    public TID Id { get; set; }
    public string? DefaultCultureCode { get; set; } = "ar-EG";
    public string Languages { get; set; } = string.Empty;
    public ICollection<TLocalizedEntity>? Locals { get; set; } = new List<TLocalizedEntity>();

}
