using static sda_3_online_Backend_Teamwork.src.DTO.BrandDTO;
using sda_3_online_Backend_Teamwork.src.Repository;
using sda_3_online_Backend_Teamwork.src.Utils;
using AutoMapper;

namespace sda_3_online_Backend_Teamwork.src.Services.Brand
{
    public class BrandService : IBrandService
    {
        protected readonly BrandRepository _brandRepo;
        protected readonly IMapper _mapper;

        public BrandService(BrandRepository brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        // Create a new brand
        public async Task<BrandReadDto> CreateOneBrandAsync(BrandCreateDto createBrandDto)
        {
            var brand = _mapper.Map<BrandCreateDto, Entity.Brand>(createBrandDto);
            var brandCreated = await _brandRepo.CreateBrand(brand); // Updated to use repo's method
            if (brandCreated == null)
            {
                throw CustomException.CreationFailed("Failed to create brand");
            }
            return _mapper.Map<Entity.Brand, BrandReadDto>(brandCreated);
        }

        // Get all brands
        public async Task<List<BrandReadDto>> GetAllBrandsAsync()
        {
            var brandList = await _brandRepo.GetAllBrandsAsync();
            if (brandList == null || !brandList.Any())
            {
                throw CustomException.NotFound("No brands found");
            }
            return _mapper.Map<List<Entity.Brand>, List<BrandReadDto>>(brandList);
        }

        // Get brand by id
        public async Task<BrandReadDto> GetBrandByIdAsync(Guid id)
        {
            var foundBrand = await _brandRepo.GetBrandById(id);
            if (foundBrand == null)
            {
                throw CustomException.NotFound($"Brand with ID {id} not found");
            }
            return _mapper.Map<Entity.Brand, BrandReadDto>(foundBrand);
        }

        // Delete a brand
        public async Task<bool> DeleteOneBrandAsync(Guid id)
        {
            var foundBrand = await _brandRepo.GetBrandById(id);
            if (foundBrand == null)
            {
                throw CustomException.NotFound($"Brand with ID {id} not found");
            }
            var isDeleted = await _brandRepo.DeleteBrand(foundBrand);
            if (!isDeleted)
            {
                throw CustomException.DeletionFailed($"Failed to delete brand with ID {id}");
            }
            return isDeleted;
        }

        // Update a brand
        public async Task<bool> UpdateOneBrandAsync(Guid id, BrandUpdateDto updateBrandDto)
        {
            var foundBrand = await _brandRepo.GetBrandById(id);
            if (foundBrand == null)
            {
                throw CustomException.NotFound($"Brand with ID {id} not found");
            }

            _mapper.Map(updateBrandDto, foundBrand);
            var isUpdated = await _brandRepo.UpdateBrand(foundBrand);
            if (!isUpdated)
            {
                throw CustomException.UpdateFailed($"Failed to update brand with ID {id}");
            }
            return isUpdated;
        }
    }
}
