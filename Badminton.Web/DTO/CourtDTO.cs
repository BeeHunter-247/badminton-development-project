using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO
{
    public class CourtDTO
    {
        public int CourtId { get; set; }

        public string CourtName { get; set; }

        public int CourtManagerId { get; set; }

        public string Location { get; set; }

        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public string Image { get; set; }

        public string Announcement { get; set; }

        public List<EvaluateDTO> Evaluates { get; set; }
        public List<SubCourtDTO> SubCourts { get; set; }
    }

    public class CreateCourtDTO
    {
        public string CourtName { get; set; }

        public int CourtManagerId { get; set; }

        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải chính xác 10 số.")]
        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public string Image { get; set; }

        public string Announcement { get; set; }
    }

    public class UpdateCourtDTO
    {
        public string CourtName { get; set; }

        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải chính xác 10 số.")]
        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public string Image { get; set; }

        public string Announcement { get; set; }
    }
}
