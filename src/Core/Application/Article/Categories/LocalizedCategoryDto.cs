namespace FSH.WebApi.Application.Article.Categories;

public class LocalizedCategoryDto : IDto
{
    public string culturCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}