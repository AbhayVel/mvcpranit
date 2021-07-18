using FirstUtility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFirst.Filters
{
    public class AuthorizationMyFilter : Attribute, IAuthorizationFilter
    {

        public string RoleList { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInRole(RoleList))
            {
                // context.Result = new UnauthorizedResult();

                context.Result = new RedirectToRouteResult(new
                {
                    controller = "Other",
                    action = "Index"
                });
            }

        }
    }
}
