using static sda_3_online_Backend_Teamwork.src.DTO.UserDTO;

namespace sda_3_online_Backend_Teamwork.src.Services.User
{
    public interface IUserService
    {
        Task<UserReadDto> CreateOneAsync(UserCreateDto createDto);
        Task<UserReadDto> CreateAdminAsync(UserCreateDto createDto);
        Task<List<UserReadDto>> GetAllAsync();
        Task<UserReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto);
        Task<string> SignInAsync(SignInDto createDto);
    }
}
