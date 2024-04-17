using FSH.WebApi.Application.Common.Localization;

namespace FSH.WebApi.Application.Keywords;
public class BaseKeywordRequest<LocalizedKeywordDto> : LocalizedRequest
{
    public new ICollection<LocalizedKeywordDto> Locals { get; set; } = new List<LocalizedKeywordDto>();
}
