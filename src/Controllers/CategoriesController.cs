using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sda_3_online_Backend_Teamwork.src.Entity;
using sda_3_online_Backend_Teamwork.src.Services.Category;
using static sda_3_online_Backend_Teamwork.src.DTO.CategoryDTO;

namespace sda_3_online_Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        /**private static List<Category> categories = new List<Category>
        {
            new Category {
                //  CategoryId = 1,
            CategoryName = "Phones" },
            new Category {
                // CategoryId = 2,
                CategoryName = "Electronic" },
        };**/

        protected readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService service)
        {
            _categoryService = service;
        }

        //create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryReadDto>> CreateOne(
            [FromBody] CategoryCreateDto createDto
        )
        {
            var categoryCreated = await _categoryService.CreateOneAsync(createDto);
            return Created($"api/v1/categories/{categoryCreated.CategoryId}", categoryCreated);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetAll()
        {
            var categoryList = await _categoryService.GetAllAsync();
            return Ok(categoryList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetById([FromRoute] Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateCategory(
            [FromRoute] Guid id,
            [FromBody] CategoryUpdateDto updateCategory
        )
        {
            bool isUpdated = await _categoryService.UpdateOneAsync(id, updateCategory);
            if (isUpdated)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCategory([FromRoute] Guid id)
        {
            bool isDeleted = await _categoryService.DeleteOneAsync(id);
            if (isDeleted)
                return NoContent();
            else
                return NotFound();
        }
    }
}
//This comment from Areej
