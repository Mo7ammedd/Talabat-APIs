using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Core.Specifications.ProductSpecifications;

public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
{
    public ProductWithBrandAndCategorySpecifications(string sort, int? brandId, int? categoryId)
        : base(p =>
            (!brandId.HasValue || p.BrandId == brandId) &&
            (!categoryId.HasValue || p.CategoryId == categoryId))
    {
        AddIncludes();
        switch (sort)
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