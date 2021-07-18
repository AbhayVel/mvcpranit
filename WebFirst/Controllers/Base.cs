using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstEnity;
using FirstUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace WebFirst.Controllers
{
    public class Base : Controller
    {

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }



        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    EndTime = DateTime.Now;
        //    context.HttpContext.Response.Headers.Add("TimeTaken", (EndTime - StartTime).TotalMilliseconds.ToString());
        //}
        ////
        //// Summary:
        ////     Called before the action method is invoked.
        ////
        //// Parameters:
        ////   context:
        ////     The action executing context.
        //[NonAction]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //try
            //{
            //    if (context.HttpContext.Session.Keys.Contains("userString"))
            //    {
            //        var userstring = "";
            //        userstring = context.HttpContext.Session.GetString("userString");

            //        LoginUserIdentity Login = JsonConvert.DeserializeObject< LoginUserIdentity>(userstring);

            //        context.HttpContext.User = new PClaimsPrincipal(Login);
            //        return;
            //    }
            //}
            //catch(Exception ex)
            //{

            //}
             

            //context.Result = new RedirectToRouteResult(new
            //{
            //    controller = "login",
            //    action = "index"

            //});
        }
    }
}
