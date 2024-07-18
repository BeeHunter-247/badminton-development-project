using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Badminton.Web.DTO
{
    public class CourtDTO
    {
        public int CourtId { get; set; }

        public string CourtName { get; set; }

        public int OwnerId { get; set; }

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

        public int OwnerId { get; set; }

        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits starting with 0.")]
        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public string? Image { get; set; }

        public string Announcement { get; set; }

        [NotMapped]
        public IFormFileCollection? formFiles { get; set; }
    }

    public class UpdateCourtDTO
    {
        public string CourtName { get; set; }

        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits starting with 0.")]
        public string Phone { get; set; }

        public string OpeningHours { get; set; }

        public string? Image { get; set; }

        public string Announcement { get; set; }

        [NotMapped]
        public IFormFileCollection? formFiles { get; set; }
    }
}
