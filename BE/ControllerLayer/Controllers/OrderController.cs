﻿using BusinessLogicLayer.Contracts;
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

        [HttpGet]
        [Route("buyer/orders")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult> GetAllOrdersByByer()
        {
            var userId = User.FindFirst("UserId").Value;
            return Ok(await _orderService.GetAllByBuyerId(userId));
        }

        [HttpPut]
        [Route("cancel")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult> CancelOrder([FromBody] CancelOrder order)
        {
            order.UserId = User.FindFirst("UserId").Value;
            return Ok(await _orderService.CancelOrder(order));
        }


        [HttpGet]
        [Route("details")]
        [Authorize(Roles = "Buyer, Admin")]
        public async Task<ActionResult> OrderDetails(string orderId)
        {
            var order = new OrderDetailsInbound() { OrderId = orderId, UserId = User.FindFirst("UserId").Value };
            return Ok(await _orderService.OrderDetails(order));
        }

        [HttpGet]
        [Route("seller/new-orders")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> GetNewOrdersBySeller()
        {
            var userId = User.FindFirst("UserId").Value;
            return Ok(await _orderService.GetNewOrdersForSeller(userId));
        }

        [HttpGet]
        [Route("seller/details")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> SellerOrderDetails(string orderId)
        {
            var order = new OrderDetailsInbound() { OrderId = orderId, UserId = User.FindFirst("UserId").Value };
            return Ok(await _orderService.SellerOrderDetails(order));
        }

    }
}
