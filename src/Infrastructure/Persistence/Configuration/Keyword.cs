using FSH.WebApi.Domain.Keywords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
internal class KeywordConfig : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> builder)
    {
        
    }
}

internal class LocalizedKeywordConfig : IEntityTypeConfiguration<LocalizedKeyword>
{
    public void Configure(EntityTypeBuilder<LocalizedKeyword> builder)
    {
        builder.Property(e => e.culturCode).HasMaxLength(3);
        builder.HasIndex(e => e.culturCode);
    }
}
