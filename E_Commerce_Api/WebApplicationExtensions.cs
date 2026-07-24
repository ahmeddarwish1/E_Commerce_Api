using System.Runtime.CompilerServices;
using E_Commerce_Domain.Contract;

namespace E_Commerce_Api
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication>SeedAndMigrationData(this WebApplication app)
        {
            using var scope=app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.SeedDataAsync();
            return app;
        }
    }
}
