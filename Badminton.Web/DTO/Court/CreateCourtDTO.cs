namespace Badminton.Web.DTO.Court
{
    public class CreateCourtDTO
    {
        public string CourtName { get; set; }

        public int CourtManagerId { get; set; }

        public string Location { get; set; }

        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public decimal PricePerHour { get; set; }

        public string Image { get; set; }

        public string Announcement { get; set; }
    }
}
