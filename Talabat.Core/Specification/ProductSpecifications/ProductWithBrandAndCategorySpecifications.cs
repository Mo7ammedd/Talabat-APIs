using Talabat.Core.Models;

namespace Talabat.Core.Specification.ProductSpecifications;

public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
{
    public ProductWithBrandAndCategorySpecifications() 
        : base()
    {
        AddIncludes();
    }


    public ProductWithBrandAndCategorySpecifications(int id )
        :base(p=>p.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.Brand);
        Includes.Add(p => p.Category);
    }
}
