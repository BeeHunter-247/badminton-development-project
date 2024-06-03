using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.SubCourt
{
    public class UpdateSCourtDTO
    {
        public string Name { get; set; }

        [Required]
        [Range(1, 200)]
        public decimal PricePerHour { get; set; }
    }
}
