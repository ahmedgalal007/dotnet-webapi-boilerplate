using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Common.Models;
public class BaseIdDto<TId> : IDto
{
    public required TId Id { get; set; }

    // TODO :public Guid CreatedBy { get; set; }
    // TODO :public DateTime CreatedOn { get; private set; }
    // TODO :public Guid LastModifiedBy { get; set; }
    // TODO :public DateTime? LastModifiedOn { get; set; }
    // TODO :public DateTime? DeletedOn { get; set; }
    // TODO :public Guid? DeletedBy { get; set; }
}
