﻿using System;

namespace FSH.WebApi.Domain.Schemas.Things.CreativeWorks.Articles;
public class AdvertiserContentArticle : Article
{
    public override string TypeName { get; protected set; } = nameof(AdvertiserContentArticle);
}
