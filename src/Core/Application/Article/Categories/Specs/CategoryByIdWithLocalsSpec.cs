using FSH.WebApi.Application.Article.Categories;
using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.Specs;
public class CategoryByIdWithLocalsSpec : Specification<Category>, ISingleResultSpecification
{
    public CategoryByIdWithLocalsSpec(Guid id) =>
        Query
            .Include(e => e.Locals)
            .Where(p => p.Id == id);
}