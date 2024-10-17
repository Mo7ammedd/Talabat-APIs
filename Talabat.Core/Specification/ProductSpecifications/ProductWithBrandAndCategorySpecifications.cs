using Talabat.Core.Models;

namespace Talabat.Core.Specification.ProductSpecifications;

public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
{
    public ProductWithBrandAndCategorySpecifications() : base()
    {
        Includes.Add(p => p.Brand);
        Includes.Add(p => p.Category);
    }

}