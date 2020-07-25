using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MicShop.Core.Data;
using MicShop.Core.Helpers;
using MicShop.Services.Enjections;

using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MicShop
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


            services.AddDbContext<MicShopContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MicShopContext")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
        }
        );
            

            services.AddServicesDependencyInjections();
            services.AddLocalization(options => options.ResourcesPath = "LangRes");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                    new CultureInfo("hy")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "hy", uiCulture: "hy");
                options.SupportedUICultures = supportedCultures;
                options.SupportedCultures = supportedCultures;
            });
            services.AddControllersWithViews()
                .AddViewLocalization();
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
            }
            app.UseStaticFiles();

 
            //var supportedCultures = new[]
            //{
            //    //new CultureInfo("en-US"),
            //    new CultureInfo("en"),
            //    //new CultureInfo("ru-RU"),
            //    new CultureInfo("ru"),
            //    //new CultureInfo("am-AM"),
            //    new CultureInfo("am")
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("am"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //});

            
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);


            app.UseRouting();



            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Shop}/{action=Index}/{id?}");
            });
        }
    }
}
