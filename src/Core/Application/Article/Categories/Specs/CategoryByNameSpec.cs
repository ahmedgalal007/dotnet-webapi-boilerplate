using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Categories.Specs;

public class CategoryByNameSpec : Specification<Category>, ISingleResultSpecification
{
    public CategoryByNameSpec(string name) => Query.Where(b => b.Locals.Any(c => c.Name == name));
}