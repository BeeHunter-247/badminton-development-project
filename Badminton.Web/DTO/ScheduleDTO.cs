namespace Badminton.Web.DTO
{
    public class ScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public int SubCourtId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal TotalHours { get; set; }
        public string BookingType { get; set; }

        /*public UserDTO User { get; set; }*/
        public SubCourtDTO SubCourt { get; set; }
    }
    public class CreateScheduleDTO
    {
        public int UserId { get; set; }
        public int SubCourtId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal TotalHours { get; set; }
        public string BookingType { get; set; }
    }

    public class UpdateScheduleDTO
    {
        public int ScheduleId { get; set; }

        public DateOnly? BookingDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public decimal? TotalHours { get; set; }
        public string BookingType { get; set; }
    }
}
