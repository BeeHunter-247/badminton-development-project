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
        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }

    public class UpdateTimeSlotDTO
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
