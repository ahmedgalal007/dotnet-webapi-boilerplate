using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Categories{
    public class CategoryDto : IDto
    {
        public Guid? Id { get; set; }
        public string? Slug { get; set; }
        public string? Color { get; set; }
        public int? DisplayOrder { get; set; } = 0;
        public Guid? ParentId { get; set; }
        public CategoryDto? Parent { get; set; }
        public List<LocalizedCategoryDto>? Locals { get; set; }
    }
}