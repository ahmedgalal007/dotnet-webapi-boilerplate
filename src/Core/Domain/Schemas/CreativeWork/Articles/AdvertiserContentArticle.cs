﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.CreativeWork.Articles;
public class AdvertiserContentArticle : Article
{
    public override string TypeName { get; protected set; } = nameof(AdvertiserContentArticle);
}
