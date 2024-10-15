using AutoMapper;
using sda_3_online_Backend_Teamwork.src.Entity;
using sda_3_online_Backend_Teamwork.src.Repository;
using sda_3_online_Backend_Teamwork.src.Utils;
using static sda_3_online_Backend_Teamwork.src.DTO.UserDTO;

namespace sda_3_online_Backend_Teamwork.src.Services.User
{
    public class UserService : IUserService
    {
        protected readonly UserRepository _userRepo;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;

        public UserService(UserRepository userRepo, IMapper mapper, IConfiguration config)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = config;
        }

        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Email))
            {
                throw CustomException.BadRequest("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(createDto.Password))
            {
                throw CustomException.BadRequest("Password is required.");
            }

            // Check if user already exists
            var existingUser = await _userRepo.FindByEmailAsync(createDto.Email);
            if (existingUser != null)
            {
                throw CustomException.Conflict(
                    $"User with email {createDto.Email} already exists."
                );
            }

            var user = _mapper.Map<UserCreateDto, Entity.User>(createDto);

            //hash password
            PasswordUtils.HashPassword(
                createDto.Password,
                out string hashedPassword,
                out byte[] salt
            );
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.Customer;
            var savedUser = await _userRepo.CreateOneAsync(user);
            return _mapper.Map<Entity.User, UserReadDto>(savedUser);
        }

        public async Task<UserReadDto> CreateAdminAsync(UserCreateDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Email))
            {
                throw CustomException.BadRequest("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(createDto.Password))
            {
                throw CustomException.BadRequest("Password is required.");
            }

            // Check if user already exists
            var existingUser = await _userRepo.FindByEmailAsync(createDto.Email);
            if (existingUser != null)
            {
                throw CustomException.Conflict(
                    $"User with email {createDto.Email} already exists."
                );
            }

            if (!(createDto.Email == "Admin@gmail.com") && !(createDto.Password == "Admin"))
            {
                throw CustomException.UnAuthorized("Incorrect admin credentials");
            }
            var user = _mapper.Map<UserCreateDto, Entity.User>(createDto);

            //hash password
            PasswordUtils.HashPassword(
                createDto.Password,
                out string hashedPassword,
                out byte[] salt
            );
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.Admin;
            var savedUser = await _userRepo.CreateOneAsync(user);
            return _mapper.Map<Entity.User, UserReadDto>(savedUser);
        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var userList = await _userRepo.GetAllAsync();
            return _mapper.Map<List<UserReadDto>>(userList);
            ;
        }

        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound($"User with id {id} cannot be found.");
            }
            return _mapper.Map<Entity.User, UserReadDto>(foundUser);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound($"User with id {id} cannot be found.");
            }

            return await _userRepo.DeleteOneAsync(foundUser);
        }

        public async Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound($"User with id {id} cannot be found.");
            }

            _mapper.Map(updateDto, foundUser);
            return await _userRepo.UpdateOneAsync(foundUser);
        }

        public async Task<string> SignInAsync(SignInDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Email))
            {
                throw CustomException.BadRequest("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(createDto.Password))
            {
                throw CustomException.BadRequest("Password is required.");
            }

            var foundUser = await _userRepo.FindByEmailAsync(createDto.Email);
            if (foundUser == null)
            {
                throw CustomException.NotFound(
                    $"User with email {createDto.Email} does not exist."
                );
            }

            var isMatched = PasswordUtils.VerifyPassword(
                createDto.Password,
                foundUser.Password,
                foundUser.Salt
            );
            if (!isMatched)
            {
                throw CustomException.UnAuthorized("Password does not match.");
            }

            // Create token
            var tokenUtil = new TokenUtils(_config);
            return tokenUtil.GenerateToken(foundUser);
        }
    }
}
