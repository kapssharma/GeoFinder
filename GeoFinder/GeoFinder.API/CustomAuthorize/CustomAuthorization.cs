using GeoFinder.Data;
using GeoFinder.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Authorization;

namespace GeoFinder.API
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        private ApplicationDbContext Context;
        public CustomAuthorization( ApplicationDbContext _context)
        {
            Context = _context;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // authorization
            string user = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(user))
            {
                context.Result = new JsonResult(new { message = "Please Enter Token" }) { StatusCode = StatusCodes.Status400BadRequest };
            }
            else if (!string.IsNullOrEmpty(user))
            {
                var split = user.Split("Bearer");
                user = split[1];
                var isExist = Context.Tokens.Select(x => x.Id.ToString() == user.Trim()).FirstOrDefault();
                if (!isExist)
                {
                    context.Result = new JsonResult(new { message = "You have Enter Invalid Token" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}