namespace FSH.WebApi.Application.Article.Categories{
    public class CategoryDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}