
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Middleware
{
    public class AuthenticatedMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticatedMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            List<string> paths = new List<string>();
            paths.Add("activities");
            paths.Add("categories");
            paths.Add("members");
            paths.Add("comments");
            paths.Add("news");
            paths.Add("users");
            paths.Add("organizations");
            paths.Add("roles");
            paths.Add("slides");


            string path = context.Request.Path;
            string[] splittedPath = path.Split("/");

            if (paths.Contains(splittedPath[2]) || paths.Contains(splittedPath[2].ToLower()))
            {
                var idUsuario = context.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

                if(idUsuario == null)
                {
                    context.Response.StatusCode = 403;
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