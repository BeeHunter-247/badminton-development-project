using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public string? Image { get; set; }

        public string Announcement { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; }
    }

    public class UpdateCourtDTO
    {
        public string CourtName { get; set; }

        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public string? Image { get; set; }

        public string Announcement { get; set; }
    }
}
