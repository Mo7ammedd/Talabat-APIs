using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecifications;

namespace Talabat.APIs.Controllers;

[Route($"api/[controller]")]
[ApiController] 
public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepo , IMapper mapper)
    {
        _productRepo = productRepo;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductWithBrandAndCategorySpecifications();
        var products = await _productRepo.GetAllWithSpecAsync(spec);
        var productsToReturn = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
        return Ok(productsToReturn);
    }
   
   
    [ProducesResponseType(typeof(ProductToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    [HttpGet("{id}")]
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
} 