using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Middleware
{
    public class AdminAccess
    {
        private readonly RequestDelegate _next;

        public AdminAccess(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var paramId = (string)context.Request.RouteValues["id"];

            List<string> methods = new List<string>();
            methods.Add("put");
            methods.Add("post");
            methods.Add("patch");
            methods.Add("delete");

            var method = context.Request.Method;

            List<string> paths = new List<string>();
            paths.Add("/activities");
            paths.Add("/category");
            paths.Add("/comments");
            paths.Add("/news");

            string path = context.Request.Path.ToString().Replace("/" + paramId, "");

            if (methods.Contains(method.ToLower()) && paths.Contains(path.ToLower()))
            {
                if (!context.User.IsInRole("1"))
                {
                    context.Response.StatusCode = 401;
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
