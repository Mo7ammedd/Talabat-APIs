using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Address = Talabat.Core.Models.Order_Aggregate.Address;

namespace Talabat.APIs.Controllers
{
  
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,
        IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order),200)]
        [ProducesResponseType(typeof(ApiResponse),400)]

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
           var order =  await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

           if (order is null) return BadRequest(new ApiResponse(400));

           return Ok(_mapper.Map<Order, OrderToReturnDto>(order));

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string Email)
        {
            var orders = await _orderService.GetOrdersForUserAsync(Email);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders)); 
        }
        [ProducesResponseType(typeof(Order),200)]
        [ProducesResponseType(typeof(ApiResponse),400)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id, string Email)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id, Email);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
        
    }
}