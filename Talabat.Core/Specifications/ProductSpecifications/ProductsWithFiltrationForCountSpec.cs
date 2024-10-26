

using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Core.Specifications.ProductSpecifications;

public class ProductsWithFiltrationForCountSpec : BaseSpecifications<Product>
{
    public ProductsWithFiltrationForCountSpec(ProductsSpecParams specParams)
        : base(p =>
            (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search.ToLower())) &&
            (!specParams.brandId.HasValue || p.BrandId == specParams.brandId) &&
            (!specParams.categoryId.HasValue || p.CategoryId == specParams.categoryId))
    {
        
    }
}