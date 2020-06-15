using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using MicShop.Helpers;
using MicShop.Services.Implamentantions;
using MicShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Services.Enjections
{
   public static class MicShopServicesInjections
    {
        public static void AddServicesDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }
    }
}
