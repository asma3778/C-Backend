using static sda_3_online_Backend_Teamwork.src.DTO.CategoryDTO;

namespace sda_3_online_Backend_Teamwork.src.Services.Category
{
    public interface ICategoryService
    {
        //send name only(to the database using repo) and return id and name (from database using repo)
        Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createdDto);

        //return named and ids
        Task<List<CategoryReadDto>> GetAllAsync();

        //return name and id
        Task<CategoryReadDto> GetByIdAsync(Guid id);

        //return type same as method in repo
        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updatedDto);
    }
}
