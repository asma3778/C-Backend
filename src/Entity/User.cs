using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace sda_3_online_Backend_Teamwork.src.Entity
{
    public class User
    {
        
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[]? Salt { get; set; }
        public Role Role {get; set;} = Role.Customer;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Role {
        Admin,
        Customer 
    }
}