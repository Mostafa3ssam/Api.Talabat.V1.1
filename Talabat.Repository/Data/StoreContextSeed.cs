using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.OrderAggrecate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.Brands.Any())
            {
                
            
            var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

            if (Brands?.Count>0)
            {
                foreach (var Brand in Brands)
                { 
                    await dbcontext.Set<ProductBrand>().AddAsync(Brand);
                }
                await dbcontext.SaveChangesAsync();

            }
            }
            if (!dbcontext.Types.Any())
            {
                var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(Type);
                    }
                    await dbcontext.SaveChangesAsync();

                } 
            }
            if (!dbcontext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dbcontext.Set<Product>().AddAsync(Product);
                    }
                    await dbcontext.SaveChangesAsync();

                } 
            } if (!dbcontext.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                        await dbcontext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
                    }
                    await dbcontext.SaveChangesAsync();

                } 
            }
        }
    }
}
