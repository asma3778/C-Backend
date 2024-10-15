using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using sda_3_online_Backend_Teamwork.src.DataBase;
using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Repository
{
    public class UserRepository
    {
        protected DbSet<User> _user;
        protected DataBaseContext _databaseContext;

        public UserRepository(DataBaseContext dataBaseContext)
        {
            _databaseContext = dataBaseContext;
            _user = dataBaseContext.Set<User>();
        }

        public async Task<User> CreateOneAsync(User newUser)
        {
            await _user.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _user.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _user.FindAsync(id);
        }

        public async Task<bool> DeleteOneAsync(User user)
        {
            _user.Remove(user);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(User user)
        {
            _user.Update(user);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //find by email
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _user.FirstOrDefaultAsync(u => u.Email == email);
        }

        //sing in
    }
}
