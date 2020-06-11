using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
