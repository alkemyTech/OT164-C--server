
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Middleware
{
    public static class MiddlewareExtension
    {

        public static IApplicationBuilder UseMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticatedMiddleware>();
        }
    }
}