using AutoMapper;
using sda_3_online_Backend_Teamwork.src.Repository;
using static sda_3_online_Backend_Teamwork.src.DTO.CategoryDTO;
using static sda_3_online_Backend_Teamwork.src.Entity.Category;
using sda_3_online_Backend_Teamwork.src.Utils;

namespace sda_3_online_Backend_Teamwork.src.Services.Category
{
    public class CategoryService : ICategoryService
    {
        protected readonly CategoryRepository _categoryRepository;
        protected readonly IMapper _mapper;

        public CategoryService(CategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        //send name only(to the database using repo) and return id and name (from database using repo)
        public async Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createdDto)
        {
            var category = _mapper.Map<CategoryCreateDto, Entity.Category>(createdDto);
            var categoryCreated = await _categoryRepository.CreateOneAsync(category);
            return _mapper.Map<Entity.Category, CategoryReadDto>(categoryCreated);
        }

        //return named and ids
        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories == null || categories.Count == 0)
            {
                throw CustomException.NotFound("No categories found");
            }
            return _mapper.Map<List<Entity.Category>, List<CategoryReadDto>>(categories);
        }

        //return name and id
        public async Task<CategoryReadDto> GetByIdAsync(Guid id)
        {
            var foundCategory = await _categoryRepository.GetByIdAsync(id);
            if (foundCategory == null)
            {
                throw CustomException.NotFound($"Category with ID {id} not found");
            }
            return _mapper.Map<Entity.Category, CategoryReadDto>(foundCategory);
        }

        //return type same as method in repo
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCategory = await _categoryRepository.GetByIdAsync(id);
            if (foundCategory == null)
            {
                throw CustomException.NotFound($"Category with ID {id} not found");
            }
            bool isDeleted = await _categoryRepository.DeleteOneAsync(foundCategory);
            if (!isDeleted)
            {
                throw CustomException.InternalError("Failed to delete category");
            }
            return isDeleted;
        }

        public async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updatedDto)
        {
            var foundCategory = await _categoryRepository.GetByIdAsync(id);
            if (foundCategory == null)
            {
                throw CustomException.NotFound($"Category with ID {id} not found");
            }
            _mapper.Map(updatedDto, foundCategory);
            return await _categoryRepository.UpdateOneAsync(foundCategory);
        }
    }
}
