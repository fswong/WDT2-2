using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Assignment2.Models;
using Assignment2.Models.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assignment2.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            await SeedRoles(serviceProvider);
            SeedApplication(serviceProvider);
        }

        private static async Task EnsureUserHasRole(
            UserManager<ApplicationUser> userManager, string userName, string role
            )
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null && !await userManager.IsInRoleAsync(user, role)) {
                await userManager.AddToRoleAsync(user,role);
            }
        }

        private static async Task SeedRoles(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] {
                Constants.CustomerRole, Constants.FranchiseHolderRole, Constants.OwnerRole
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            await EnsureUserHasRole(userManager, "customer@example.com", Constants.CustomerRole);
            await EnsureUserHasRole(userManager, "owner@example.com", Constants.OwnerRole);
            await EnsureUserHasRole(userManager, "s3593297@student.rmit.edu.au", Constants.FranchiseHolderRole);
        }

        public static void SeedApplication(IServiceProvider serviceProvider) {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any products.
                if (context.Products.Any())
                {
                    return; // DB has been seeded.
                }

                var products = new[]
                {
                    new Product
                    {
                        Name = "Rabbit",
                        Price = 15.00
                    },
                    new Product
                    {
                        Name = "Hat",
                        Price = 12.00
                    },
                    new Product
                    {
                        Name = "Svengali Deck",
                        Price = 9.00
                    },
                    new Product
                    {
                        Name = "Floating Hankerchief",
                        Price = 5.00
                    },
                    new Product
                    {
                        Name = "Wand",
                        Price = 10.00
                    },
                    new Product
                    {
                        Name = "Broomstick",
                        Price = 16.00
                    },
                    new Product
                    {
                        Name = "Bang Gun",
                        Price = 20.00
                    },
                    new Product
                    {
                        Name = "Cloak of Invisibility",
                        Price = 13.00
                    },
                    new Product
                    {
                        Name = "Elder Wand",
                        Price = 12.00
                    },
                    new Product
                    {
                        Name = "Resurrection Stone",
                        Price = 8.00
                    }
                };

                context.Products.AddRange(products);

                var i = 0;
                context.OwnerInventories.AddRange(
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 20
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 50
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 100
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 150
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 40
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 10
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 5
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 0
                    },
                    new OwnerInventory
                    {
                        Product = products[i++],
                        StockLevel = 0
                    },
                    new OwnerInventory
                    {
                        Product = products[i],
                        StockLevel = 0
                    }
                );

                i = 0;
                var stores = new[]
                {
                    new Store
                    {
                        Name = "Melbourne CBD",
                        StoreInventories =
                        {
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 15
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 10
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 5
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 5
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 5
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 5
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 5
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 1
                            },
                            new StoreInventory
                            {
                                Product = products[i++],
                                StockLevel = 1
                            },
                            new StoreInventory
                            {
                                Product = products[i],
                                StockLevel = 1
                            },
                        }
                    },
                    new Store
                    {
                        Name = "North Melbourne",
                        StoreInventories =
                        {
                            new StoreInventory
                            {
                                Product = products[0],
                                StockLevel = 5
                            }
                        }
                    },
                    new Store
                    {
                        Name = "East Melbourne",
                        StoreInventories =
                        {
                            new StoreInventory
                            {
                                Product = products[1],
                                StockLevel = 5
                            }
                        }
                    },
                    new Store
                    {
                        Name = "South Melbourne",
                        StoreInventories =
                        {
                            new StoreInventory
                            {
                                Product = products[2],
                                StockLevel = 5
                            }
                        }
                    },
                    new Store
                    {
                        Name = "West Melbourne"
                    }
                };

                context.Stores.AddRange(stores);

                context.SaveChanges();
            }
        
        }
    }
}
