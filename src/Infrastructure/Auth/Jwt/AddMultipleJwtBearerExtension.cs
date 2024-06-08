using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Auth.Jwt;
public static class AddMultipleJwtBearerExtension
{
    public static AuthenticationBuilder AddMultipleJwtBearer(this AuthenticationBuilder builder)
    {

        return builder;
    }
}
