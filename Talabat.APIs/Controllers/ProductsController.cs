using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;

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
       var products = await _productRepo.GetAllAsync();
       return Ok(products);
   }
   [HttpGet("{id}")]
   public async Task <ActionResult<Product>> GetProduct(int id)
   {
       var product = await _productRepo.GetAsync(id);
       
         if(product == null)
         {
              return NotFound(new {Message = "not found ", StatusCode = 404});//404
         }
       return Ok(product);//200
   }
}