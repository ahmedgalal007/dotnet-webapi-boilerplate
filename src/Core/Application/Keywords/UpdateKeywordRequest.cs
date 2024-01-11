using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class UpdateKeywordRequest : IRequest<Guid>
{
}

public class UpdateKeywordRequestValidation : CustomValidator<UpdateKeywordRequest>
{
}
public class UpdateKeywordRequestHandler : IRequestHandler<UpdateKeywordRequest, Guid>
{
    public Task<DefaultIdType> Handle(UpdateKeywordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

