using Microsoft.EntityFrameworkCore;
using sda_3_online_Backend_Teamwork.src.DataBase;
using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Repository
{
    public class BrandRepository
    {
        protected DbSet<Brand> _brands;
        protected DataBaseContext _databaseContext;

        public BrandRepository(DataBaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _brands = databaseContext.Set<Brand>();
        }

        // Create a new brand
        public async Task<Brand> CreateBrand(Brand newBrand)
        {
            await _brands.AddAsync(newBrand);
            await _databaseContext.SaveChangesAsync();
            return newBrand;
        }

        // Get a brand by ID
        public async Task<Brand?> GetBrandById(Guid id)
        {
            return await _brands.FindAsync(id);
        }

        // Delete a brand
        public async Task<bool> DeleteBrand(Brand brand)
        {
            _brands.Remove(brand);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // Update a brand
        public async Task<bool> UpdateBrand(Brand updatedBrand)
        {
            _brands.Update(updatedBrand);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // Get all brands
        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await _brands.ToListAsync();
        }

        // Internal async methods to handle future enhancements (like in your OrderRepository)
        internal async Task<Brand> CreateOneBrandAsync(Brand brand)
        {
            throw new NotImplementedException();
        }

        internal async Task<bool> DeleteOneBrandAsync(Brand foundBrand)
        {
            throw new NotImplementedException();
        }

        internal async Task<Brand> GetBrandByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        internal async Task<bool> UpdateOneBrandAsync(Brand foundBrand)
        {
            throw new NotImplementedException();
        }
    }
}
