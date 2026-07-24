using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.DataSeeding;
using E_Commerce_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce_Infrastructure
{
    public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {

                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");


            return services;
        }



    }
}
