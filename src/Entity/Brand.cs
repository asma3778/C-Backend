namespace sda_3_online_Backend_Teamwork.src.Entity
{
    public class Brand
    {
      public Guid BrandId { get; set; } = Guid.NewGuid();
      public required string BrandName { get; set; }
      public required string Description { get; set; }


      public ICollection<Product> Products { get; set; } = new List<Product>();
        public Guid CategoryId { get; set; } // FK to Category
        public Category? Category { get; set; } // Navigation property
    }
}
