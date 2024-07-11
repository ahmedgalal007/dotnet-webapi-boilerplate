using FSH.WebApi.Application.Common.Localization;

namespace FSH.WebApi.Application.Keywords;
public class BaseCategoryRequest<LocalizedCategoryDto> : LocalizedRequest
{
    public new ICollection<LocalizedCategoryDto> Locals { get; set; } = new List<LocalizedCategoryDto>();
}
