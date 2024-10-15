using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using sda_3_online_Backend_Teamwork.src.DataBase;
using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.Repository
{
    public class OrderRepository
    {
        protected DbSet<Order> _order;
        protected DataBaseContext _databaseContext;

        public OrderRepository(DataBaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _order = databaseContext.Set<Order>();
        }

        public async Task<Order> CreateOneOrderAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            // await _order.Entry(newOrder).Collection(o => o.OrderItems).LoadAsync();

            /* foreach (var item in newOrder.OrderItems)
             {
                 await _databaseContext.Entry(item).Reference(oi => oi.Product).LoadAsync();
             }
 
             return newOrder;*/
            var orderWithDetails = await _order
                .Include(o => o.OrderItems)
                .ThenInclude(Od => Od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);
            return orderWithDetails;
        }

        public async Task<List<Order>> GetAllOrdersAsync(string searchTerm = null)
        {
            var query = _order.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Filter by 'Status' or any other string property you want to search by
                query = query.Where(o => o.Status.Contains(searchTerm));
            }

            return await query.ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _order
                .Include(o => o.OrderItems)
                .ThenInclude(Od => Od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            //throw new NotImplementedException();
        }

        public async Task<bool> DeleteOneOrderAsync(Order order)
        {
            _order.Remove(order);
            await _databaseContext.SaveChangesAsync();
            return true;
            //throw new NotImplementedException();
        }

        public async Task<bool> UpdateOneOrderAsync(Order updatedOrder)
        {
            _order.Update(updatedOrder);
            await _databaseContext.SaveChangesAsync();
            return true;
            //throw new NotImplementedException();
        }

        public async Task<List<Order>> GetOrdersByIdAsync(Guid userId)
        {
            return await _order
                .Include(o => o.OrderItems)
                .ThenInclude(Od => Od.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
