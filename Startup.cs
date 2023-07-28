using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPDMS.ExceptionHandlear;
using Microsoft.AspNetCore.Authentication.Cookies;
using CPDMS.Model.ExceptionList;
using CPDMS.Areas.UnitRegistration.Models.UnitRegistration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using CPDMS.Models.Utility;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Options;
using CPDMS.Services;
using CPDMS.ActionFilter;
using CPDMSEF;
using CPDMS.Areas.CompanyRegistraion.Models.UnitRegistration;
using CPDMS.Areas.LandingPage.Models.Login;
using CPDMS.Areas.CMS.Models.Chemical;
//using CPDMS.Models.SMS;

namespace CPDMS
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

          //  services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Configuration.GetSection("KeyList:RingPaths").Value))
          //.SetApplicationName("SharedCookieApp");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            });

            services.AddAuthentication("Identity.Application").AddCookie("Identity.Application", options =>
            {
                options.Cookie.Name = ".AspNet.SharedCookie";//Configuration.GetSection("KeyList:CookieName").Value;
                //options.LoginPath = "/Home/Index";
                //options.LogoutPath = "/Home/Index";
                //options.Cookie.Domain = Configuration.GetSection("KeyList:DomenNAme").Value;
                options.Cookie.HttpOnly = true;
            });
            //services.AddDataProtection();
            #region bilingual
            services.AddSingleton<CommonLocalizationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("hi")
                    };
                    opt.DefaultRequestCulture = new RequestCulture("en");
                    opt.SupportedCultures = supportedCultures;
                    opt.SupportedUICultures = supportedCultures;
                });

            #endregion
            services.AddHttpClient();
            services.AddDistributedMemoryCache();
           
            services.AddScoped<IExceptionProvider, ExceptionProvider>();
            services.AddScoped<IHttpWebClients, HttpWebClients>();
            services.AddScoped<IUnitModel, UnitModel>();
            services.AddScoped<ICompanyModel, CompanyModel>();
            services.AddScoped<ILoginModel, LoginModel>();
            services.AddScoped<IChemicalModel, ChemicalModel>();
           // services.AddScoped<SMSService>();
            #region Website module
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(ExceptionFilterHandlear));
                    config.Filters.Add(typeof(SessionActionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.IgnoreNullValues = true;
                   options.JsonSerializerOptions.WriteIndented = true;
               });
            services.AddRazorPages();

            services.Configure<KeyList>(Configuration.GetSection("KeyList"));

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            #region bilingual
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            #endregion
            app.UseSession();
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseIdentity();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(routes =>
            {

                routes.MapControllerRoute(
                   name: "default",
                   //pattern: "{area=UnitRegistration}/{controller=UnitRegistration}/{action=UnitsEntryAdd}/{id?}");
                   //  pattern: "{area=UnitRegistration}/{controller=UnitRegistration}/{action=UnitTRegistration}/{id?}");
                   //pattern: "{area=companyregistraion}/{controller=companyregistration}/{action=companytregistration}/{id?}");
                   pattern: "{area=LandingPage}/{controller=Home}/{action=LandingPage}/{id?}");
                    //pattern: "{area=LandingPage}/{controller=Home}/{action=Login}/{id?}");
        //pattern: "{area=LandingPage}/{controller=Home}/{action=Test}/{id?}");
            //pattern: "{controller=Home}/{action=Login}/{id?}");
            //pattern: "{controller=Home}/{action=Index}/{id?}");


            //routes.MapControllerRoute(
            //     name: "default1",
            //     pattern: "{controller=Home}/{action=Index}/{id?}");



            //routes.MapControllerRoute(
            //     name: "ssss",
            //     pattern: "/{controller=Home}/{action=Index}/{id?}");
            //routes.MapAreaControllerRoute(
            //     name: "default",
            //      areaName: "Website",
            //     pattern: "Website/{controller=Website}/{action=Home}/{id?}");


        });

        }
    }
}
