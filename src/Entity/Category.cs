namespace sda_3_online_Backend_Teamwork.src.Entity
{
    public class Category
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public required string CategoryName { get; set; }

      public ICollection<Product> Products { get; set; } = new List<Product>();


    }
}