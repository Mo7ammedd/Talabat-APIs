using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specifications.ProductSpecifications;

namespace Talabat.APIs.Controllers;

[Route($"api/[controller]")]
[ApiController] 
public class ProductsController : BaseApiController
{

    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ProductsController(IMapper mapper,IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }
    [Cached(300)]
    [HttpGet] // api/products
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductsSpecParams specParams)
    {
        var products = await _productService.GetProductsAsync(specParams);
        var count = await _productService.GetCountAsync(specParams) ;                 
        var data  = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, data, count));
    }
   
    [ProducesResponseType(typeof(ProductToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    [HttpGet("{id:int}")] // api/products/1
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);

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
        return Ok(await _productService.GetBrandsAsync());
    }
    
    [HttpGet("categories")] // api/products/categories  
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
    {
        return Ok(await _productService.GetCategoryAsync());
    }
    
} 