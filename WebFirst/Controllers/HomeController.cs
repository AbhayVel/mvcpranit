using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebFirst.Models;
using WebFirst.Utility;

namespace WebFirst.Controllers
{

    public class HomeController : Base
    {
        public int i = 0;

        public int j = 0;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Employee e = new Employee()
            {
                Id = 1,
                Name = "ABhay",
                OtherAddress =  new List<Address> { 
                    new Address { HomeAddress ="a1", OfficeAddress= "cd" },
                new Address { HomeAddress ="B2", OfficeAddress= "HJ" },
                 new Address { HomeAddress ="C3", OfficeAddress= "XX" },
                }
                
            };
            return View(e);
        }


        [HttpPost]
        public async Task<IActionResult> Index(Employee e)
        {

            Employee e2 = new Employee();
            MyModelBinder.Bind(e2, Request, null, null);
            Address a2 = new Address();
            MyModelBinder.Bind(a2, Request, null, null, "Address");

            Country c2 = new Country();
            MyModelBinder.Bind(c2, Request, null, null, "Address.Country");



            return View(e);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
