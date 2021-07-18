using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DBContextProject;
using FirstRepositoryLayer;
using FirstService;
using FirstUtility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebFirst
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<EmployeeContext, EmployeeContext>();
             services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeprtmentRepo, DepartmentRepo>();
            services.AddTransient<IPrincipal, PClaimsPrincipal>();
            services.AddTransient<ClaimsPrincipal, PClaimsPrincipal>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x =>
             {

                 x.LoginPath = "/Login/index";
                 x.LogoutPath = "/login/index";
                 x.AccessDeniedPath = "/Unauth/index";

                 x.ExpireTimeSpan = TimeSpan.FromMinutes(20);
             });

           
          //  services.AddScoped<CustomCookieAuthenticationEvents>();
            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromMinutes(20);
            });

       //     services.AddControllersWithViews();
            //IDeprtmentRepo

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            //app.UseCookiePolicy(new CookiePolicyOptions
            //{
            //    MinimumSameSitePolicy = SameSiteMode.Strict,
            //});

            app.UseAuthentication();        //    app.UseClaimsMiddleware();
            app.Use(async (context, next) =>
            {
                
              PClaimsPrincipal p = new PClaimsPrincipal(context.User.Identities);
                context.User = p;
                
                await next();
            });

            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Index}/{id?}");
            });
        }
    }




    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        

        public CustomCookieAuthenticationEvents()
        {
             
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            //    var userPrincipal = context.Principal;

            // Look for the LastChanged claim.
            //var lastChanged = (from c in userPrincipal.Claims
            //                   where c.Type == "LastChanged"
            //                   select c.Value).FirstOrDefault();

             
           //     context.RejectPrincipal();

            //    await context.HttpContext.SignOutAsync(
              //      CookieAuthenticationDefaults.AuthenticationScheme);
            
        }
    }
}
