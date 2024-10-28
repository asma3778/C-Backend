using AutoMapper;
using static sda_3_online_Backend_Teamwork.src.DTO.CategoryDTO;
using static sda_3_online_Backend_Teamwork.src.DTO.OrderDTO;
using static sda_3_online_Backend_Teamwork.src.DTO.OrderItemDTO;
using static sda_3_online_Backend_Teamwork.src.DTO.ProductDTO;
using static sda_3_online_Backend_Teamwork.src.DTO.UserDTO;

namespace sda_3_online_Backend_Teamwork.src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            // Category
             CreateMap<Entity.Category, CategoryReadDto>();
             CreateMap<CategoryCreateDto, Entity.Category>();
             CreateMap<CategoryUpdateDto, Entity.Category>().
             ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

            // Product
            CreateMap<Entity.Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Entity.Product>();
            CreateMap<ProductUpdateDto, Entity.Product>().
            ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

            // User
            CreateMap<Entity.User, UserReadDto>();
            CreateMap<UserCreateDto, Entity.User>();
            CreateMap<UserUpdateDto, Entity.User>().
            ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

            // Order
             CreateMap<Entity.Order, OrderReadDto>();
             CreateMap<OrderCreateDto, Entity.Order>();
             CreateMap<OrderUpdateDto, Entity.Order>().
             ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

            // OrderItem
             CreateMap<Entity.OrderItem, OrderItemReadDto>();
             CreateMap<OrderItemCreateDto, Entity.OrderItem>().
          //   CreateMap<OrderItemUpdateDto, Entity.OrderItem>().
             ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
        }
    }
}
