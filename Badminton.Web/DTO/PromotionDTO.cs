using Badminton.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO
{
    public class PromotionDTO
    {
        public int PromotionId { get; set; }

        public int CourtId { get; set; }

        public string PromotionName { get; set; }

        public string Description { get; set; }

        public decimal? DiscountPercentage { get; set; }

        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

    }

    public class CreatePromotionDTO
    {
        public int CourtId { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public String? PromotionName { get; set; }

        public String? Description { get; set; }

        public decimal DiscountPercentage { get; set; }

        public String? StartDate { get; set; }

        public String? EndDate { get; set; }
    }

    public class UpdatePromotionDTO
    {
        public String? PromotionName { get; set; }

        public String? Description { get; set; }

        public decimal DiscountPercentage { get; set; }

        public String? StartDate { get; set; }

        public String? EndDate { get; set; }
    }

}
