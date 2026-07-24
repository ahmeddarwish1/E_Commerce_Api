using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities;
using E_Commerce_Domain.Entities.Products;
using E_Commerce_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace E_Commerce_Domain.DataSeeding
{
    public class CatalogDataSeeder(StoreDbContext dbContext, ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingmigration = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingmigration.Any())
                    await dbContext.Database.MigrateAsync();

                var seedroot = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                //var path = Path.Combine(seedroot, "Products.json");
                await SeedIfEmptyAsync<ProductBrand, int>(seedroot, "brands.json");
                await SeedIfEmptyAsync<ProductType, int>(seedroot, "types.json");
                await SeedIfEmptyAsync<Product, int>(seedroot, "products.json");
                int result=await dbContext.SaveChangesAsync(ct);
                if (result > 0)
                    logger.LogInformation($"{result} rows added");
                else
                    logger.LogInformation($"Database alredy seed");
            }
            catch
            {

            }
        }


        private async Task SeedIfEmptyAsync<T, TKey>(string rootpath, string filename, CancellationToken ct = default) where T : BaseEntity<TKey>
        {
            if (await dbContext.Set<T>().AnyAsync())
            {

                logger.LogInformation("Table already has data");
                return;
            }
            var filepath = Path.Combine(rootpath, filename); 
            if (!File.Exists(filepath))
            {
                logger.LogWarning("File not exist");
                return;
            }
            using var filestram = File.OpenRead(filepath);
            var options = new JsonSerializerOptions()
            {

                PropertyNameCaseInsensitive = true
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(filestram, options, ct);
            if (items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);

        }
    }
}
