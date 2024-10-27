using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs;

public class BasketItemDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string productName { get; set; }
    [Required]
    public string PictureUrl { get; set; }
    [Required]
    [Range(0.1,double.MaxValue,ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    [Range(1,int.MaxValue,ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
     
}