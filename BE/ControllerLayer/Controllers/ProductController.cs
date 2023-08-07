using BusinessLogicLayer.Contracts;
using Common.Models.Inbound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    [EnableCors("AllowAll")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet]
        [Route("seller/products")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> GetAllBySeller()
        {
            var userId = User.FindFirst("UserId").Value;
            return Ok(await _productService.GetAllBySellerId(userId.ToString()));
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> AddNewProduct([FromForm] NewProduct product)
        {
            product.SellerId = User.FindFirst("UserId").Value;
            return Ok(await _productService.AddNewProduct(product));
        }

        [HttpPut]
        [Route("products/{productId}/update")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> Update([FromForm] UpdateProduct product)
        {
            product.SellerId = User.FindFirst("UserId").Value;
            return Ok(await _productService.Update(product));
        }

        [HttpDelete]
        [Route("products/{productId}/delete")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> Delete(DeleteProduct product)
        {
            product.SellerId = User.FindFirst("UserId").Value;
            return Ok(await _productService.Delete(product));
        }
    }
}
