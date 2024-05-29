using Badminton.Web.DTO.Evaluate;
using Badminton.Web.Models;

namespace Badminton.Web.DTO.Court
{
    public class CourtDTO
    {
        public int CourtId { get; set; }

        public string CourtName { get; set; }

        public int CourtManagerId { get; set; }

        public string Location { get; set; }

        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public decimal PricePerHour { get; set; }

        public string Image { get; set; }

        public string Announcement { get; set; }

        public List<EvaluateDTO> Evaluates { get; set; }
    }
}
