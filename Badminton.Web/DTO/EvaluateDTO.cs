using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO
{
    public class EvaluateDTO
    {
        public int EvaluateId { get; set; }

        public int? CourtId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime EvaluateDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }

    }

    public class CreateEvaluateDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime EvaluateDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
    }

    public class UpdateEvaluateDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime EvaluateDate { get; set; } = DateTime.Now;
    }
}
