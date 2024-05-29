namespace Badminton.Web.DTO.Evaluate
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
}
