using Badminton.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO
{
    public class CheckInDTO
    {
        public int CheckInId { get; set; }

        public int SubCourtId { get; set; }

        public int BookingId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CheckInTime { get; set; }

        [Required]
        [Range(0, 1)]
        public bool CheckInStatus { get; set; }

        public int UserId { get; set; }
    }

    public class CreateCheckInDTO
    {
        public int SubCourtId { get; set; }

        public int BookingId { get; set; }

        [DataType(DataType.DateTime)]
        public string CheckInTime { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class UpdateCheckInDTO
    {
        public bool CheckInStatus { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CheckInTime { get; set; } = DateTime.Now;

    }
}
