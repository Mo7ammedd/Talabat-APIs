using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecifications;

namespace Talabat.APIs.Controllers;

[Route($"api/[controller]")]
[ApiController] 
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepo;

    public ProductsController(IGenericRepository<Product> productRepo)
    {
        _productRepo = productRepo;
    }
    
    
   [HttpGet]
   public async Task <ActionResult<IEnumerable<Product>>> GetProducts()
   {
       var spec = new ProductWithBrandAndCategorySpecifications();
       var products = await _productRepo.GetAllWithSpecAsync(spec);
       return Ok(products);
   }
   
   
   [HttpGet("{id}")]
   public async Task <ActionResult<Product>> GetProduct(int id)
   {
       var spec = new ProductWithBrandAndCategorySpecifications(id);
       var product = await _productRepo.GetWithSpecAsync(spec);
       
         if(product == null)
         {
              return NotFound(new {Message = "not found ", StatusCode = 404});
         }
       return Ok(product);//200
   }
}