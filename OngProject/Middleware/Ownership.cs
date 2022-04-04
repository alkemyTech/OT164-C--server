using Microsoft.AspNetCore.Http;
using OngProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Security.Claims;

namespace OngProject.Middleware
{
    public class Ownership
    {
        private readonly RequestDelegate _next;

        public Ownership(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethod.Put.Method || context.Request.Method == HttpMethod.Delete.Method)
            {
                var role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var claimId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var paramId = (string)context.Request.RouteValues["id"];
                if (paramId != null && paramId != "")
                {
                    var excludePaths = new List<string>() { "/users" };
                    var currentPath = context.Request.Path.ToString().ToLower();
                    foreach (var path in excludePaths)
                    {
                        if (currentPath.Contains(path))
                        {
                            if (Int32.Parse(claimId) != Int32.Parse(paramId) && !role.Value.Equals("1"))
                            {
                                context.Response.StatusCode = 403;
                                return;
                            }
                        }
                    }
                }
            }
            await _next.Invoke(context);
        }
    }
}
