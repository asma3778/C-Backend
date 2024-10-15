using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.DTO
{
    public class UserDTO
    {
        public class UserCreateDto
        {
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public byte[]? Salt { get; set; }
            
            
            public ICollection<Order> Orders { get; set; } = new List<Order>();
        }

        public class UserReadDto
        {
            public Guid UserId { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public Role Role { get; set; }

        }

        public class UserUpdateDto
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Address { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
        public class SignInDto
        {

            public required string Email { get; set; }
            public required string Password { get; set; }

        }
    }
}
