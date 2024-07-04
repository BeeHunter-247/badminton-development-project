using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO
{
    public class TimeSlotDTO
    {
        public int TimeSlotId { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }

    public class CreateTimeSlotDTO
    {
        [Required]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public string EndTime { get; set; }
    }

    public class UpdateTimeSlotDTO
    {
        [Required]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public string EndTime { get; set; }
    }
}
