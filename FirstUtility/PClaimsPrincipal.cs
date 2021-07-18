using FirstEnity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FirstUtility
{


    public static class ClaimsPrincipalStatic
    {
        public static bool IsInRole1(this ClaimsPrincipal claimsPrincipal, string role)
        {

            string[] rolesArray = role.Split(',').Select(x => x.Trim().ToLower()).ToArray();


            return rolesArray.Contains(claimsPrincipal.Claims.ToList().FirstOrDefault(X => X.Type == "Role").Value.ToLower());
        }


        public static async Task SigninAsync(this HttpContext httpContext, LoginUserIdentity loginUserIdentity)
        {
            var ClaimList = new List<Claim>();
            ClaimsIdentity identity = new ClaimsIdentity(ClaimList, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal user = new PClaimsPrincipal(identity, loginUserIdentity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
        }

    }
   public class PClaimsPrincipal : ClaimsPrincipal
    {

        //public List<Claim> ClaimList { get; set; } 
        //public override IEnumerable<Claim> Claims { get {  return ClaimList; } }
     //  public ClaimsIdentity ClaimsIdentity { get { return LoginUserIdentity;  } }


        public ClaimsIdentity ClaimsIdentity { get; set; }
        public  LoginUserIdentity LoginUserIdentity { get; set; }

        //public PClaimsPrincipal() : base()
        //{

        //}

        public PClaimsPrincipal(IEnumerable<ClaimsIdentity> identities) : base(identities)
        {
            LoginUserIdentity = new LoginUserIdentity();


        }
            public PClaimsPrincipal(ClaimsIdentity ClaimsIdentity, LoginUserIdentity identity) : base(ClaimsIdentity)
        {
            this.LoginUserIdentity = identity;
            // this.LoginUserIdentity = identity;

            LoginUserIdentity.Address = "Kharadi";

            this.ClaimsIdentity = ClaimsIdentity;
            this.ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Name, identity.Name));
            this.ClaimsIdentity.AddClaim(new Claim("Role", identity.Role));
            this.ClaimsIdentity.AddClaim(new Claim("Address", identity.Address));
            // ClaimsIdentity.


        }

        public override bool IsInRole(string role)
        {

            string[] rolesArray = role.Split(',').Select(x => x.Trim().ToLower()).ToArray();

            var claim = this.Claims.ToList().FirstOrDefault(X => X.Type == "Role");

            if (claim != null)
            {
                return rolesArray.Contains(this.Claims.ToList().FirstOrDefault(X => X.Type == "Role").Value.ToLower());
            }
            return false;
           
        }

        //////public PClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
        //////{
             
        //////}
    }



}
