using Badminton.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO
{
    public class ScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public int SubCourtId { get; set; }
        
        [DataType(DataType.Date)]
        public DateOnly BookingDate { get; set; }
        public decimal TotalHours { get; set; }
        public string BookingType { get; set; }
        public int TimeSlotId { get; set; }

    }
    public class CreateScheduleDTO
    {
        public int UserId { get; set; }
        public int SubCourtId { get; set; }
        public string BookingDate { get; set; }
        public int TimeSlotId { get; set; }
        public decimal TotalHours { get; set; }
        public BookingType? BookingType { get; set; }
    }

    public class UpdateScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int TimeSlotId { get; set; }
        public string BookingDate { get; set; }
        public decimal? TotalHours { get; set; }
        public BookingType? BookingType { get; set; }
    }
}
