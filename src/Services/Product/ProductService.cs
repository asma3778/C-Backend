using AutoMapper;
using sda_3_online_Backend_Teamwork.src.Entity;
using sda_3_online_Backend_Teamwork.src.Repository;
using static sda_3_online_Backend_Teamwork.src.DTO.ProductDTO;
using sda_3_online_Backend_Teamwork.src.Utils;

namespace sda_3_online_Backend_Teamwork.src.Services.Product
{
    public class ProductService : IProductService
    {
        protected readonly ProductRepository _productRepo;
        protected readonly IMapper _mapper;

        public ProductService(ProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductReadDto> CreateOneProductAsync(ProductCreateDto createProductDto)
        {
            var product = _mapper.Map<ProductCreateDto, Entity.Product>(createProductDto);
            var productCreated = await _productRepo.CreateOneProductAsync(product);
            return _mapper.Map<Entity.Product, ProductReadDto>(productCreated);
        }
        public async Task<List<ProductReadDto>> GetAllProductsAsync(string searchTerm)
        {
            var products = await _productRepo.GetAllProductsAsync(searchTerm);
            return _mapper.Map<List<Entity.Product>, List<ProductReadDto>>(products);
        }


        public async Task<object> GetProductsWithFiltersAsync(ProductQueryParameters queryParameters)
        {
            var productList = await _productRepo.GetAllProductsAsync(queryParameters.SearchTerm);

            // Filter by price range
            if (queryParameters.MinPrice.HasValue)
            {
                productList = productList.Where(p => p.Price >= queryParameters.MinPrice.Value).ToList();
            }
            if (queryParameters.MaxPrice.HasValue)
            {
                productList = productList.Where(p => p.Price <= queryParameters.MaxPrice.Value).ToList();
            }

            // Sorting
            productList = queryParameters.SortOrder.ToLower() == "desc"
                ? productList.OrderByDescending(p => GetSortValue(p, queryParameters.SortBy)).ToList()
                : productList.OrderBy(p => GetSortValue(p, queryParameters.SortBy)).ToList();

            // Pagination
            var totalItems = productList.Count;
            var paginatedProducts = productList
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToList();

            // Create response object
            return new
            {
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)queryParameters.PageSize),
                CurrentPage = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize,
                Products = _mapper.Map<List<Entity.Product>, List<ProductReadDto>>(paginatedProducts)
            };
        }

        private object GetSortValue(Entity.Product product, string sortBy)
        {
            return sortBy.ToLower() switch
            {
                "price" => product.Price,
                _ => product.ProductName, // Default sort by title
            };
        }

        public async Task<ProductReadDto> GetProductByIdAsync(Guid id)
        {
            var foundProduct = await _productRepo.GetProductByIdAsync(id);
            if (foundProduct == null)
            {
                throw CustomException.NotFound($"Product with ID {id} can't be found.");
            }
            return _mapper.Map<Entity.Product, ProductReadDto>(foundProduct);
        }

        public async Task<bool> DeleteOneProductAsync(Guid id)
        {
            var foundProduct = await _productRepo.GetProductByIdAsync(id);
            if (foundProduct == null)
            {
                throw CustomException.NotFound($"Product with ID {id} can't be found.");
            }

            return await _productRepo.DeleteOneProductAsync(foundProduct);
        }

        public async Task<bool> UpdateOneProductAsync(Guid id, ProductUpdateDto updateProductDto)
        {
            var foundProduct = await _productRepo.GetProductByIdAsync(id);
            if (foundProduct == null)
            {
                throw CustomException.NotFound($"Product with ID {id} can't be found.");
            }

            _mapper.Map(updateProductDto, foundProduct);
            return await _productRepo.UpdateOneProductAsync(foundProduct);
        }
    }
}
