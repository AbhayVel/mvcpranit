using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace WebFirst.Filters
{
    public class MyActionFIlter : Attribute, IActionFilter
    {


        public double MinValue { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            EndTime = DateTime.Now;

            var time = (EndTime - StartTime).TotalMilliseconds;
            if(time> MinValue)
            {
                context.HttpContext.Response.Headers.Add("TimeTaken", (EndTime - StartTime).TotalMilliseconds.ToString());
            }

            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            StartTime = DateTime.Now;
        }
    }
}
