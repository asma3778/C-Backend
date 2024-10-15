using static sda_3_online_Backend_Teamwork.src.DTO.ProductDTO;
using sda_3_online_Backend_Teamwork.src.Utils;

namespace sda_3_online_Backend_Teamwork.src.Services.Product
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateOneProductAsync(ProductCreateDto createProductDto);
        Task<List<ProductReadDto>> GetAllProductsAsync(string searchTerm);
        Task<ProductReadDto> GetProductByIdAsync(Guid id);
        Task<bool> DeleteOneProductAsync(Guid id);
        Task<bool> UpdateOneProductAsync(Guid id, ProductUpdateDto updateProductDto);
        Task<object> GetProductsWithFiltersAsync(ProductQueryParameters queryParameters);
    }
}
