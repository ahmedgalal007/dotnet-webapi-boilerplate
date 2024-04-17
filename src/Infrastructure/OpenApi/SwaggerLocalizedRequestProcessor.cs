using Namotion.Reflection;
using NSwag.Generation.Processors.Contexts;
using NSwag.Generation.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.OpenApi;

public sealed class SwaggerLocalizedRequestProcessor<T> : IDocumentProcessor where T : class
{
    public void Process(DocumentProcessorContext context)
    {
        context.SchemaGenerator.Generate(typeof(T).ToContextualType(), context.SchemaResolver);
    }
}