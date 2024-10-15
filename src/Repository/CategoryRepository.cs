using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using sda_3_online_Backend_Teamwork.src.DataBase;
using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Repository
{
    public class CategoryRepository
    {
        protected DbSet<Category> _category;
        protected DataBaseContext _databaseContext;

        public CategoryRepository(DataBaseContext dataBaseContext)
        {
            _databaseContext = dataBaseContext;
            _category = dataBaseContext.Set<Category>();
        }

        public async Task<Category> CreateOneAsync(Category newCategory)
        {
            await _category.AddAsync(newCategory);
            await _databaseContext.SaveChangesAsync();
            return newCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _category.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _category.FindAsync(id);
        }

        public async Task<bool> DeleteOneAsync(Category category)
        {
            _category.Remove(category);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Category category)
        {
            _category.Update(category);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
