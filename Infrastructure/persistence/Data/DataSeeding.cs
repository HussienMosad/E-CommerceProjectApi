using Domain.Contracts;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace persistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var BrandsData = File.OpenRead("..\\Infrastructure\\persistence\\Data\\DataSeed\\brands.json");

                    var Brands =  await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);

                    if (Brands is not null && Brands.Any())
                        await _dbContext.AddRangeAsync(Brands);

                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var TypesData = File.OpenRead("..\\Infrastructure\\persistence\\Data\\DataSeed\\types.json");

                    var Types = await  JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);
                    if (Types is not null && Types.Any())
                       await  _dbContext.AddRangeAsync(Types);
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.OpenRead("..\\Infrastructure\\persistence\\Data\\DataSeed\\products.json");

                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                       await _dbContext.AddRangeAsync(Products);
                }

               await  _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The Error IS : {ex}");
            }
            
        }
    }
}
