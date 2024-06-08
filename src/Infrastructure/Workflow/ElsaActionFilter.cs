//using FSH.WebApi.Application.Common.Interfaces;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc;

//namespace FSH.WebApi.Infrastructure.Workflow;
//public class ElsaActionFilter : IAsyncActionFilter, ITransientDependency
//{
//    private readonly ICurrentUser _currentUser;
//    private readonly IPermissionChecker _permissionChecker;

//    public ElsaActionFilter(IPermissionChecker permissionChecker, ICurrentUser currentUser)
//    {
//        _permissionChecker = permissionChecker;
//        _currentUser = currentUser;
//    }

//    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        if (context.Controller.GetType().FullName.StartsWith("Elsa.Server.Api.Endpoints"))
//        {
//            //elsa api endpoint
//            if (!_currentUser.IsAuthenticated())
//            {
//                context.Result = new UnauthorizedResult();
//                return;
//            }

//            if (!await _permissionChecker.IsGrantedAsync("PermissionName..."))
//            {
//                context.Result = new UnauthorizedResult();
//                return;
//            }

//            await next();
//        }
//        else
//        {
//            await next();
//        }
//    }
//}