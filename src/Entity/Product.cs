namespace sda_3_online_Backend_Teamwork.src.Entity
{
    public class Product
    {
        public Guid ProductId { get; set; } 
        //= Guid.NewGuid();
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }

        public Guid CategoryId { get; set; } // FK
        public Category Category { get; set; } // Navigation property
    }  
}