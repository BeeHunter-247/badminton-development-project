namespace Badminton.Web.Helpers
{
    public class QueryOptions
    {
        public string? search { get; set; }
        public decimal? from { get; set; }
        public decimal? to { get; set; }
    }

    public class QueryCourt
    {
        public string?  search { get; set; }
    }
}
