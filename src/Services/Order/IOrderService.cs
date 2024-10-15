using static sda_3_online_Backend_Teamwork.src.DTO.OrderDTO;

namespace sda_3_online_Backend_Teamwork.src.Services.Order
{
    public interface IOrderService
    {
        Task<OrderReadDto> CreateOneOrderAsync(Guid userId, OrderCreateDto createOrderDto);

        //get order list by user id
        Task<List<OrderReadDto>> GetAllOrdersAsync();
        Task<OrderReadDto> GetOrderByIdAsync(Guid id);
        Task<bool> DeleteOneOrderAsync(Guid id);
        Task<bool> UpdateOneOrderAsync(Guid id, OrderUpdateDto updateOrderDto);
        Task<List<OrderReadDto>> GetOrdersByIdAsync(Guid userId);
    }
}
