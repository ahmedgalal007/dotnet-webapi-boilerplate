using FSH.WebApi.Application.Keywords;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
namespace FSH.WebApi.Application.Common.Localization;
public class LocalizedRequest : LocalizedRequest<DefaultIdType, LocalizedDto> { }
public class LocalizedRequest<TID, TLocalizedDto> : IRequest<TID>
    where TLocalizedDto: LocalizedDto
{
    public TID Id { get; set; }
    public string? DefaultCultureCode { get; set; } = "ar-EG";
    public string Languages { get; set; } = string.Empty;
    public new ICollection<TLocalizedDto> Locals { get; set; } = new List<TLocalizedDto>();

}
