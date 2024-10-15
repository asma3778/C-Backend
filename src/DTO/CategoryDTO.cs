using sda_3_online_Backend_Teamwork.src.Entity;


namespace sda_3_online_Backend_Teamwork.src.DTO
{
    public class CategoryDTO
    {
        public class CategoryCreateDto
        {
            public required string CategoryName { get; set; }

        public ICollection<Brand> Brands { get; set; } = new List<Brand>();

        }

        public class CategoryReadDto
        {
            public Guid CategoryId { get; set; }
            public string CategoryName { get; set; }
        }

        public class CategoryUpdateDto
        {
            public required string CategoryName { get; set; }
        }
    }
}
