using sda_3_online_Backend_Teamwork.src.Services.Brand;
using static sda_3_online_Backend_Teamwork.src.DTO.BrandDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace sda_3_online_Backend_Teamwork.src.Controllers

{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: api/v1/brands
        [HttpGet]
        public async Task<ActionResult> GetBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        // GET: api/v1/brands/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleBrand(Guid id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            return brand is not null ? Ok(brand) : NotFound();
        }

        // POST: api/v1/brands
        [HttpPost]
        
        public async Task<ActionResult> CreateBrand(BrandCreateDto newBrandDto)
        {
            var createdBrand = await _brandService.CreateOneBrandAsync(newBrandDto);
            return CreatedAtAction(
                nameof(GetSingleBrand),
                new { id = createdBrand.BrandId },
                createdBrand
            );
        }

        // PUT: api/v1/brands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBrand(Guid id, BrandUpdateDto updatedBrandDto)
        {
            var isUpdated = await _brandService.UpdateOneBrandAsync(id, updatedBrandDto);
            return isUpdated ? Ok() : NotFound();
        }

        // DELETE: api/v1/brands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(Guid id)
        {
            var isDeleted = await _brandService.DeleteOneBrandAsync(id);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}
