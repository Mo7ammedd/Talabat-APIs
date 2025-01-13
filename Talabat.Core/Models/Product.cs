using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Models;

public class Product : BaseModel
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string PictureUrl { get; set; }
    
    public decimal Price { get; set; }
    
    public int BrandId { get; set; } // foreign key => ProductBrand
    
    public ProductBrand Brand { get; set; } // navegation property one 

    public int CategoryId { get; set; } // foreign key => ProductCategory 
    
    public ProductCategory Category { get; set; }// navegation property one 
}