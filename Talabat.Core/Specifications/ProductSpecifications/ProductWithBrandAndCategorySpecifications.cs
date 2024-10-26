using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Core.Specifications.ProductSpecifications;

public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
{
    public ProductWithBrandAndCategorySpecifications(ProductsSpecParams specParams)
        : base(p =>
            (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search.ToLower())) &&
            (!specParams.brandId.HasValue || p.BrandId == specParams.brandId.Value) &&
            (!specParams.categoryId.HasValue || p.CategoryId == specParams.categoryId.Value))
    {
        AddIncludes();
        switch (specParams.sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
        ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }

    public ProductWithBrandAndCategorySpecifications(int id)
        : base(p => p.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.Brand);
        Includes.Add(p => p.Category);
    }
}