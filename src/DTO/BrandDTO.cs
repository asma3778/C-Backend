using sda_3_online_Backend_Teamwork.src.Entity;


namespace sda_3_online_Backend_Teamwork.src.DTO
{
    public class BrandDTO
    {
       
        public class BrandCreateDto
        {
           
            public required string BrandName { get; set; }
            public required string Description { get; set; }

      public ICollection<Product> Products { get; set; } = new List<Product>();
      
        public Guid CategoryId { get; set; } // FK to Category
        public Category? Category { get; set; } // Navigation property

        }

     
        public class BrandReadDto
        {
            public Guid BrandId { get; set; }
            public string BrandName { get; set; }
            public string Description { get; set; }
        }


        public class BrandUpdateDto
        {
            public string? BrandName { get; set; }
            public string? Description { get; set; }
        }
    }
}
