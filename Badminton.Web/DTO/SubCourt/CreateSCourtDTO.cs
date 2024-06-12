using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.SubCourt
{
    public class CreateSCourtDTO
    {
        public string Name { get; set; }

        [Required]
        [Range(1, 200)]
        public decimal PricePerHour { get; set; }

        public int TimeSlotId { get; set; }
    }
}
