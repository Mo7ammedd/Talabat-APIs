using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers;

[Authorize]
public class PaymentController : BaseApiController
{
    
    private readonly IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [ProducesResponseType(typeof(CustomerBasket ), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [HttpPost("{basketId}")]
   public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
 
   {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));
        return Ok(basket);
    }
}