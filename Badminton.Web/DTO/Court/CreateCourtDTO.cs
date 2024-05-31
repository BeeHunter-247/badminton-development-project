using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.Court
{
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
}
