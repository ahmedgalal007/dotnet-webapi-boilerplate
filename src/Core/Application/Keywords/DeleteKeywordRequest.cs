using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class DeleteKeywordRequest : IRequest<Guid>
{
}

public class DeleteKeywordRequestValidation : CustomValidator<DeleteKeywordRequest>
{
}
public class DeleteKeywordRequestHandler : IRequestHandler<DeleteKeywordRequest, Guid>
{
    public Task<DefaultIdType> Handle(DeleteKeywordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

