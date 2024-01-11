using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class CreateKeywordRequest : IRequest<Guid>
{
}

public class CreateKeywordRequestValidation : CustomValidator<CreateKeywordRequest>
{
}
    public class CreateKeywordRequestHandler : IRequestHandler<CreateKeywordRequest, Guid>
{
    public Task<DefaultIdType> Handle(CreateKeywordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
