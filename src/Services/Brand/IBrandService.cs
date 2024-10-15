using static sda_3_online_Backend_Teamwork.src.DTO.BrandDTO;

namespace sda_3_online_Backend_Teamwork.src.Services.Brand
{
    public interface IBrandService
    {
        Task<BrandReadDto> CreateOneBrandAsync(BrandCreateDto createBrandDto);
        Task<List<BrandReadDto>> GetAllBrandsAsync();
        Task<BrandReadDto> GetBrandByIdAsync(Guid id);
        Task<bool> DeleteOneBrandAsync(Guid id);
        Task<bool> UpdateOneBrandAsync(Guid id, BrandUpdateDto updateBrandDto);
    }
}
