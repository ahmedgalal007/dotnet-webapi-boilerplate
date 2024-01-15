using FSH.WebApi.Domain.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.Categories.Specs;
internal class GetCategoryByIdSpec : SingleResultSpecification<Category,CategoryDto>
{
    public GetCategoryByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}
