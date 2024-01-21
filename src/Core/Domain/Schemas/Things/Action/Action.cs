using FSH.WebApi.Domain.Schemas.Things.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things;
public abstract class Action : Thing
{
    public override string TypeName { get; protected set; } = nameof(Action);
}


// TODO AchieveAction
// TODO AssessAction
// TODO ConsumeAction
// TODO ControlAction
// TODO CreateAction
// TODO FindAction
// TODO InteractAction
// TODO MoveAction
// TODO OrganizeAction
// TODO PlayAction
// TODO SearchAction
// TODO SeekToAction
// TODO SolveMathAction
// TODO TradeAction
// TODO TransferAction
// TODO UpdateAction