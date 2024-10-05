using System.Text.Json;
using Microsoft.Extensions.Logging;
using Talabat.Core.Models;

namespace Talabat.Repository.Data;

public class StoreContextSeed
{
    public async static Task SeedAsync(StoreContext _dbContext)
    {
        if (!_dbContext.ProductBrands.Any())
        {
            var brandsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands is not null && brands.Count > 0)
            {

                foreach (var brand in brands)
                {
                    _dbContext.Set<ProductBrand>().Add(brand);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
        if (!_dbContext.ProductCategories.Any())
        {
            var categoryData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);
            if (categories is not null && categories.Count > 0)
            {

                foreach (var category in categories)
                {
                    _dbContext.Set<ProductCategory>().Add(category);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
        if (!_dbContext.Products.Any())
        {
            var productData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            if (products is not null && products.Count > 0)
            {

                foreach (var product in products)
                {
                    _dbContext.Set<Product>().Add(product);
                }

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}