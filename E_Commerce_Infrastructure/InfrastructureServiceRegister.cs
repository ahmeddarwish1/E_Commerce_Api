using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Contracts;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.DataSeeding;
using E_Commerce_Domain.Entities.Identity;
using E_Commerce_Infrastructure.Data;
using E_Commerce_Infrastructure.DataSeeding;
using E_Commerce_Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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
            services.AddDbContext<StoreIdentityDbContext>(option =>
            {

                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(opt =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<ICacheRepository, CacheRepository>();
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }



    }
}
