using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sda_3_online_Backend_Teamwork.src.Services.Product;
using Microsoft.AspNetCore.Authorization;
using static sda_3_online_Backend_Teamwork.src.DTO.ProductDTO;
using sda_3_online_Backend_Teamwork.src.Utils;

namespace sda_3_online_Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductReadDto>> CreateOne(ProductCreateDto createProductDto)
        {
            var productCreated = await _productService.CreateOneProductAsync(createProductDto);
            return Ok(productCreated);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ProductQueryParameters queryParameters)
        {
            var result = await _productService.GetProductsWithFiltersAsync(queryParameters);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProductUpdateDto updateProductDto)
        {
            var result = await _productService.UpdateOneProductAsync(id, updateProductDto);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteOneProductAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
