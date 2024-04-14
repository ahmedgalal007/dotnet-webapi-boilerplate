using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
namespace FSH.WebApi.Application.Common.Localization;
public class LocalizedRequest<TID, ILocalizedDto> : IRequest<TID>
{
    public TID Id { get; set; }
    public string? DefaultCultureCode { get; set; } = "ar-EG";
    public string Languages { get; set; } = string.Empty;
    public ICollection<ILocalizedDto> Locals { get; set; } = new List<ILocalizedDto>();

}
