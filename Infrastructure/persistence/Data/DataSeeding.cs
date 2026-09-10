using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace persistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext
        , RoleManager<IdentityRole> _roleManager
        , UserManager<User> _userManager) : IDataSeeding
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

                if (!_dbContext.DeliveryMethods.Any())
                {
                   using var deliveryData = File.OpenRead(
                        "..\\Infrastructure\\persistence\\Data\\DataSeed\\delivery.json");

                    var deliveries =
                        await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(
                            deliveryData);

                    if (deliveries is not null && deliveries.Any())
                    {
                        await _dbContext.DeliveryMethods.AddRangeAsync(deliveries);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seeding Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                throw;
            }

        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                // =========================
                // Seed Roles
                // =========================

                var roles = new[]
                {
            "Admin",
            "SuperAdmin",
            "Customer"
        };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        var roleResult = await _roleManager.CreateAsync(
                            new IdentityRole(role)
                        );

                        if (!roleResult.Succeeded)
                        {
                            var errors = string.Join(
                                ", ",
                                roleResult.Errors.Select(e => e.Description)
                            );

                            Console.WriteLine(
                                $"Failed to create role '{role}': {errors}"
                            );
                        }
                    }
                }


                // =========================
                // Seed Admin User
                // =========================

                var adminEmail = "ahmed.hassan@store.com";

                var admin = await _userManager.FindByEmailAsync(adminEmail);

                if (admin is null)
                {
                    admin = new User
                    {
                        DisplayName = "Ahmed Hassan",
                        UserName = "ahmed.hassan",
                        Email = adminEmail,
                        PhoneNumber = "01012345678",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };

                    var adminResult = await _userManager.CreateAsync(
                        admin,
                        "Admin@12345"
                    );

                    if (!adminResult.Succeeded)
                    {
                        var errors = string.Join(
                            ", ",
                            adminResult.Errors.Select(e => e.Description)
                        );

                        Console.WriteLine(
                            $"Failed to create Admin user: {errors}"
                        );
                    }
                }

                if (admin is not null &&
                    !await _userManager.IsInRoleAsync(admin, "Admin"))
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }


                // =========================
                // Seed Super Admin User
                // =========================

                var superAdminEmail = "omar.mohamed@store.com";

                var superAdmin = await _userManager.FindByEmailAsync(
                    superAdminEmail
                );

                if (superAdmin is null)
                {
                    superAdmin = new User
                    {
                        DisplayName = "Omar Mohamed",
                        UserName = "omar.mohamed",
                        Email = superAdminEmail,
                        PhoneNumber = "01098765432",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };

                    var superAdminResult = await _userManager.CreateAsync(
                        superAdmin,
                        "SuperAdmin@12345"
                    );

                    if (!superAdminResult.Succeeded)
                    {
                        var errors = string.Join(
                            ", ",
                            superAdminResult.Errors.Select(e => e.Description)
                        );

                        Console.WriteLine(
                            $"Failed to create SuperAdmin user: {errors}"
                        );
                    }
                }

                if (superAdmin is not null &&
                    !await _userManager.IsInRoleAsync(
                        superAdmin,
                        "SuperAdmin"))
                {
                    await _userManager.AddToRoleAsync(
                        superAdmin,
                        "SuperAdmin"
                    );
                }


                // =========================
                // Seed Customer User
                // =========================

                var customerEmail = "mohamed.ali@gmail.com";

                var customer = await _userManager.FindByEmailAsync(
                    customerEmail
                );

                if (customer is null)
                {
                    customer = new User
                    {
                        DisplayName = "Mohamed Ali",
                        UserName = "mohamed.ali",
                        Email = customerEmail,
                        PhoneNumber = "01123456789",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };

                    var customerResult = await _userManager.CreateAsync(
                        customer,
                        "Customer@12345"
                    );

                    if (!customerResult.Succeeded)
                    {
                        var errors = string.Join(
                            ", ",
                            customerResult.Errors.Select(e => e.Description)
                        );

                        Console.WriteLine(
                            $"Failed to create Customer user: {errors}"
                        );
                    }
                }

                if (customer is not null &&
                    !await _userManager.IsInRoleAsync(
                        customer,
                        "Customer"))
                {
                    await _userManager.AddToRoleAsync(
                        customer,
                        "Customer"
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Identity Seeding Error: {ex}"
                );
            }
        }
    }
}
