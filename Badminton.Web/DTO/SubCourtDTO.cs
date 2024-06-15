using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO
{
    public class SubCourtDTO
    {
        public int SubCourtId { get; set; }

        public int CourtId { get; set; }

        public string Name { get; set; }

        public decimal PricePerHour { get; set; }

        public int TimeSlotId { get; set; }
    }

    public class CreateSubCourtDTO
    {
        public string Name { get; set; }

        [Required]
        [Range(1, 200)]
        public decimal PricePerHour { get; set; }

        public int TimeSlotId { get; set; }
    }

    public class UpdateSubCourtDTO
    {
        public string Name { get; set; }

        [Required]
        [Range(1, 200)]
        public decimal PricePerHour { get; set; }
        public int TimeSlotId { get; set; }
    }
}
