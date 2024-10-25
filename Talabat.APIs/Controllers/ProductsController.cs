using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specifications.ProductSpecifications;

namespace Talabat.APIs.Controllers;

[Route($"api/[controller]")]
[ApiController] 
public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductCategory> _productCategoryRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepo ,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductCategory> productCategoryRepo,
        IMapper mapper)
    {
        _productRepo = productRepo;
        _productBrandRepo = productBrandRepo;
        _productCategoryRepo = productCategoryRepo;
        _mapper = mapper;
    }
    
    
    [HttpGet] // api/products
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductsSpecParams specParams)
    {
        var spec = new ProductWithBrandAndCategorySpecifications(specParams);  
        var products = await _productRepo.GetAllWithSpecAsync(spec);
        var data  = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, data));
    }
   
    [ProducesResponseType(typeof(ProductToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    [HttpGet("{id:int}")] // api/products/1
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductWithBrandAndCategorySpecifications(id);
        var product = await _productRepo.GetWithSpecAsync(spec);

        if (product == null)
        {
            return NotFound(new ApiResponse(404));
        }

        var productToReturn = _mapper.Map<Product, ProductToReturnDto>(product);

        return Ok(productToReturn); // 200
    }
    
    [HttpGet("brands")] // api/products/brands
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.GetAllAsync());
    }
    
    [HttpGet("categories")] // api/products/categories  
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
    {
        return Ok(await _productCategoryRepo.GetAllAsync());
    }
    
} 