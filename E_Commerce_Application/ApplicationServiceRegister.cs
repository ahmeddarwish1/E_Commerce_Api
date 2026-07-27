using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce_Application
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegister).Assembly);
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
