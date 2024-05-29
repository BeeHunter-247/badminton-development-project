using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.Evaluate
{
    public class CreateEvaluateDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime EvaluateDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
    }
}
