namespace Badminton.Web.DTO.SubCourt
{
    public class SCourtDTO
    {
        public int SubCourtId { get; set; }

        public int CourtId { get; set; }

        public string Name { get; set; }
        public decimal PricePerHour { get; set; }
    }
}
