using Microsoft.EntityFrameworkCore;
using sda_3_online_Backend_Teamwork.src.DataBase;
using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Repository
{
    public class ProductRepository
    {
        protected DbSet<Product> _product;
        protected DataBaseContext _databaseContext;

        public ProductRepository(DataBaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _product = databaseContext.Set<Product>();
        }

        public async Task<Product> CreateOneProductAsync(Product newProduct)
        {
            await _product.AddAsync(newProduct);
            await _databaseContext.SaveChangesAsync();
            return newProduct;
        }

        // public async Task<List<Product>> GetAllProductsAsync()
        // {
        //     return await _product.ToListAsync();
        // }

        public async Task<List<Product>> GetAllProductsAsync(string searchTerm = null)
        {
            // Start with the base query
            var query = _product.AsQueryable();

            // If a search term is provided, filter the products
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p =>
                    p.ProductName.Contains(searchTerm) || p.Description.Contains(searchTerm)
                );
            }

            // Return the filtered list as a list asynchronously
            return await query.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _product
               // .Include(p => p.Brand)
               // .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<bool> DeleteOneProductAsync(Product product)
        {
            _product.Remove(product);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneProductAsync(Product updatedProduct)
        {
            _product.Update(updatedProduct);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
