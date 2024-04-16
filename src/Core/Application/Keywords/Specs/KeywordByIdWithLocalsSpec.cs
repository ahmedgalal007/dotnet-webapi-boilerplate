using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords.Specs;
public class KeywordByIdSpec : Specification<Keyword, KeywordDto>, ISingleResultSpecification
{
    public KeywordByIdSpec(Guid id) =>
        Query
            .Include(e => e.Locals)
            .Where(p => p.Id == id);
}