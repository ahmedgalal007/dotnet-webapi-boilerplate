using FSH.WebApi.Application.Common.Localization;

namespace FSH.WebApi.Application.Article.Categories;

public class LocalizedCategoryDto : LocalizedDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}