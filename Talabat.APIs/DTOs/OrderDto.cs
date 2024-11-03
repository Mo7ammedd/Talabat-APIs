using System.ComponentModel.DataAnnotations;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.APIs.DTOs;

public class OrderDto
{
   [Required]
    public string BasketId { get; set; }
    [Required]
    public int DeliveryMethodId { get; set; }
    [Required]
    public AddressDto ShippingAddress { get; set; }
}