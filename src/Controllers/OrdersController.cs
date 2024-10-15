using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sda_3_online_Backend_Teamwork.src.Services.Order;
using static sda_3_online_Backend_Teamwork.src.DTO.OrderDTO;

namespace sda_3_online_Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/v1/orders
        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/v1/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleOrder(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return order is not null ? Ok(order) : NotFound();
        }

        // POST: api/v1/orders
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderReadDto>> CreateOrder(
            [FromBody] OrderCreateDto newOrderDto
        )
        {
            //sum total amount
            //user info by token
            var authenticationClaims = HttpContext.User;
            var userId = authenticationClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;

            var userGuid = new Guid(userId);
            return await _orderService.CreateOneOrderAsync(userGuid, newOrderDto);
            /**
            var createdOrder = await _orderService.CreateOneOrderAsync(userGuid, newOrderDto);
            return CreatedAtAction(
                nameof(GetSingleOrder),
                new { id = createdOrder.OrderId },
                createdOrder
            );**/
        }

        // PUT: api/v1/orders/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(Guid id, OrderUpdateDto updatedOrderDto)
        {
            var isUpdated = await _orderService.UpdateOneOrderAsync(id, updatedOrderDto);
            return isUpdated ? Ok() : NotFound();
        }

        // DELETE: api/v1/orders/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var isDeleted = await _orderService.DeleteOneOrderAsync(id);
            return isDeleted ? NoContent() : NotFound();
        }

        //Get order list by user Id

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<List<OrderReadDto>>> GetOrdersByUserId(
            [FromRoute] Guid userId
        )
        {
            var orders = await _orderService.GetOrdersByIdAsync(userId);
            return Ok(orders);
        }
    }
}
