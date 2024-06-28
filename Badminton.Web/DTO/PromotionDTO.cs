namespace Badminton.Web.DTO
{
    public class PromotionDTO
    {
        public int PromotionId { get; set; }

        public string PromotionCode { get; set; }

        public string Description { get; set; }

        public decimal Percentage { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }

    public class UpdatePromotionDTO
    {
        public string PromotionCode { get; set; }

        public string Description { get; set; }

        public decimal Percentage { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }

    public class CreatePromotionDTO
    {
        public string PromotionCode { get; set; }

        public string Description { get; set; }

        public decimal Percentage { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
