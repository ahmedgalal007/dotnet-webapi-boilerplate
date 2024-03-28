//using FSH.WebApi.Domain.Schemas.Things;
//using FSH.WebApi.Domain.Schemas.Things.CreativeWorks;
//using FSH.WebApi.Domain.Schemas.Things.CreativeWorks.Articles;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using System.Threading.Tasks;

//namespace FSH.WebApi.Infrastructure.Persistence.Configuration.Schemas;
//public class SchemaConfig : IEntityTypeConfiguration<Thing>
//{
//    public void Configure(EntityTypeBuilder<Thing> builder)
//    {
//        builder.ToTable(nameof(Thing), SchemaNames.Schema)
//            .SplitToTable(
//                "Article",
//                tableBuilder =>
//                {
//                    // tableBuilder.spl
//                });
//    }
//}

//public class ArticleConfig : IEntityTypeConfiguration<NewsArticle>
//{
//    public void Configure(EntityTypeBuilder<NewsArticle> builder)
//    {
//        //builder.ToTable("Things");
//        //builder.Property(o => o.).HasColumnName("Status");
//        //builder.HasOne(o => o.).WithOne()
//        //    .HasForeignKey<DetailedOrder>(o => o.Id);
//        //builder.Navigation(o => o.DetailedOrder).IsRequired();
//    }
//}