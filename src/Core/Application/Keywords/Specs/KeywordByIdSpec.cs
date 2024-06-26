﻿using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords.Specs;
public class KeywordByIdWithLocalsSpec : Specification<Keyword>, ISingleResultSpecification<Keyword>
{
    public KeywordByIdWithLocalsSpec(Guid id) =>
        Query
            .Include(e => e.Locals)
            .Where(p => p.Id == id);
}