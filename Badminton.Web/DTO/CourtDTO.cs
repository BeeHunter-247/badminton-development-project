using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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

        public int Status { get; set; }

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
        public List<IFormFile>? FormFiles { get; set; } // Thay đổi ở đây
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

    public class UpdateStatusDTO
    {
        public int Status { get; set; }
    }

    public class SetDefaultStatus
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
        public int Status { get; set; }
        public List<IFormFile>? FormFiles { get; set; } // Thay đổi ở đây
    }
}
