using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
  
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (updatedBasket == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(updatedBasket);
        }
        [HttpDelete("id")]
        public async Task<ActionResult<CustomerBasket>> DeleteBasket(string id)
        {
            var deleted = await _basketRepository.DeleteBasketAsync(id);
            if (!deleted)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok();
        }
        
    }
}