using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.ProductSpecifications;

namespace Talabat.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductsSpecParams specParams)
    {
        var spec = new ProductWithBrandAndCategorySpecifications(specParams);  
        var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
        return products;
    }

    public async Task<Product?> GetProductAsync(int productId)
    {
        var spec = new ProductWithBrandAndCategorySpecifications(productId);
        var product = await _unitOfWork.Repository<Product>().GetWithSpecAsync(spec);
        return product;
    }

    public async Task<int> GetCountAsync(ProductsSpecParams specParams)
    {
        var countSpec = new ProductsWithFiltrationForCountSpec(specParams);
        var count = await _unitOfWork.Repository<Product>().GetCountAsync(countSpec);
        return count;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

    public async Task<IReadOnlyList<ProductCategory>> GetCategoryAsync()
        => await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

}