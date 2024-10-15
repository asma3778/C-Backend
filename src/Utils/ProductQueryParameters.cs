namespace sda_3_online_Backend_Teamwork.src.Utils
{
    public class ProductQueryParameters
    {
        public string ?SearchTerm { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public string SortBy { get; set; } = "title";
        public string SortOrder { get; set; } = "asc";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }
}