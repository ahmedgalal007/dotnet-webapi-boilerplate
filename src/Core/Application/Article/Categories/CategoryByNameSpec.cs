using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Categories;

public class CategoryByNameSpec : Specification<Category>, ISingleResultSpecification
{
    public CategoryByNameSpec(string name) => Query.Where(b => b.Name.Localizations.Any(c => c.Value == name));
}