namespace Badminton.Web.Helpers
{
    public class QueryOptions
    {
        public string? search { get; set; }
        public decimal? from { get; set; }
        public decimal? to { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 5;
    }

    public class QueryCourt
    {
        public string?  search { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 5;
    }
}
