using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstEnity;
using FirstUtility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebFirst.Controllers
{
    public class LoginController : Controller
    {

      public  LoginController()
        {

        }
        public IActionResult Index()
        {

            
            
            return View();
        }


        public IActionResult logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        


            public async Task<IActionResult> Login(LoginUserIdentity loginUserIdentity)
        {
            if(loginUserIdentity.UserName.Equals("abhay") && loginUserIdentity.Password.Equals("abc"))
            {

                loginUserIdentity.Role = "Admin";               

                var userString = JsonConvert.SerializeObject(loginUserIdentity);

                //  HttpContext.Session.SetString("userString", userString);
                loginUserIdentity.IsAuthenticated = true;
                loginUserIdentity.AuthenticationType = "";
                //var user = new PClaimsPrincipal(loginUserIdentity);
                await HttpContext.SigninAsync(loginUserIdentity);
                return Redirect("/user/index");
            } else if (loginUserIdentity.UserName.Equals("pradeep") && loginUserIdentity.Password.Equals("abc"))
            {

                loginUserIdentity.Role = "dept";

               // var userString = JsonConvert.SerializeObject(loginUserIdentity);
                loginUserIdentity.IsAuthenticated = true;
                loginUserIdentity.AuthenticationType = "";
               await HttpContext.SigninAsync(loginUserIdentity);
                return Redirect("/user/index");
            }
            else if (loginUserIdentity.UserName.Equals("pranit") && loginUserIdentity.Password.Equals("abc"))
            {

                loginUserIdentity.Role = "sales";

                 

                var userString = JsonConvert.SerializeObject(loginUserIdentity);
                loginUserIdentity.IsAuthenticated = true;
                loginUserIdentity.AuthenticationType = "";
                await HttpContext.SigninAsync(loginUserIdentity);
                return Redirect("/user/index");
            } else {
                ViewBag.message = "Uer name/Password is not matched";
               

                return View("Index", loginUserIdentity);
            }

            //HttpContext.Session.Set()
            //HttpContext.Session.SetString("User", "Abhay");
           
        }



    }
}
