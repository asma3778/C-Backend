using AutoMapper;
using sda_3_online_Backend_Teamwork.src.Entity;
using sda_3_online_Backend_Teamwork.src.Repository;
using sda_3_online_Backend_Teamwork.src.Utils;
using static sda_3_online_Backend_Teamwork.src.DTO.OrderDTO;

namespace sda_3_online_Backend_Teamwork.src.Services.Order
{
    public class OrderService : IOrderService
    {
        protected readonly OrderRepository _orderRepo;
        protected readonly IMapper _mapper;

        public OrderService(OrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<OrderReadDto> CreateOneOrderAsync(
            Guid userId,
            OrderCreateDto createOrderDto
        )
        {
            var order = _mapper.Map<OrderCreateDto, Entity.Order>(createOrderDto);
            order.UserId = userId;

            await _orderRepo.CreateOneOrderAsync(order);
            return _mapper.Map<Entity.Order, OrderReadDto>(order);

            //var orderCreated = await _orderRepo.CreateOneOrderAsync(order);
            //return _mapper.Map<Entity.Order, OrderReadDto>(orderCreated);
        }

        // Get all orders - returns List<OrderReadDto>
        public async Task<List<OrderReadDto>> GetAllOrdersAsync()
        {
            var orderList = await _orderRepo.GetAllOrdersAsync();
            return _mapper.Map<List<Entity.Order>, List<OrderReadDto>>(orderList);
        }

        // Get an order by its ID - returns OrderReadDto
        public async Task<OrderReadDto> GetOrderByIdAsync(Guid id)
        {
            var foundOrder = await _orderRepo.GetOrderByIdAsync(id);

            if (foundOrder == null)
            {
                throw CustomException.NotFound($"Order with ID {id} not found");
            }

            return _mapper.Map<Entity.Order, OrderReadDto>(foundOrder);
        }

        public async Task<List<OrderReadDto>> GetOrdersByIdAsync(Guid userId)
        {
            var orders = await _orderRepo.GetOrdersByIdAsync(userId);
            var orderList = _mapper.Map<List<Entity.Order>, List<OrderReadDto>>(orders);
            return orderList;
        }

        // Delete an order by its ID - returns bool
        public async Task<bool> DeleteOneOrderAsync(Guid id)
        {
            var foundOrder = await _orderRepo.GetOrderByIdAsync(id);

            if (foundOrder == null)
            {
                throw CustomException.NotFound($"Order with ID {id} not found");
            }

            bool isDeleted = await _orderRepo.DeleteOneOrderAsync(foundOrder);

            if (isDeleted)
            {
                return true;
            }

            throw CustomException.InternalError("Failed to delete order");
        }

        // Update an order by its ID - returns bool
        public async Task<bool> UpdateOneOrderAsync(Guid id, OrderUpdateDto updateOrderDto)
        {
            var foundOrder = await _orderRepo.GetOrderByIdAsync(id);

            if (foundOrder == null)
            {
                throw CustomException.NotFound($"Order with ID {id} not found");
            }

            _mapper.Map(updateOrderDto, foundOrder);
            return await _orderRepo.UpdateOneOrderAsync(foundOrder);
        }
    }
}
