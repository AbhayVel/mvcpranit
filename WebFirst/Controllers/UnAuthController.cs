using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebFirst.Controllers
{
    public class UnAuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
