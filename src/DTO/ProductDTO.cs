using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.DTO
{
    public class ProductDTO
    {
        public class ProductCreateDto
        {
            public required string ProductName { get; set; }
            public decimal Price { get; set; }
            public required string Description { get; set; }
            public int StockQuantity { get; set; }
            public string ImageUrl { get; set; }

            public Guid CategoryId { get; set; } // FK
            public Category? Category { get; set; } // Navigation property
        }

        public class ProductReadDto
        {
            public Guid ProductId { get; set; } = Guid.NewGuid();
            public required string ProductName { get; set; }
            public decimal Price { get; set; }
            public required string Description { get; set; }
            public int StockQuantity { get; set; }
            public string ImageUrl { get; set; }
            public Guid CategoryId { get; set; }
        }

        public class ProductUpdateDto
        {
            public string? ProductName { get; set; }
            public decimal? Price { get; set; }
            public string? Description { get; set; }
            public int? StockQuantity { get; set; }
            public string? ImageUrl { get; set; }
        }
    }
}
