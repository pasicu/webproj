using BusinessLogicLayer.Contracts;
using Common.Models.Inbound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using System.Data;

namespace ControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult> CreateNewOrder([FromBody] NewOrder order)
        {
            order.UserId = User.FindFirst("UserId").Value;
            return Ok(await _orderService.CreateNewOrder(order));
        }

    }
}
