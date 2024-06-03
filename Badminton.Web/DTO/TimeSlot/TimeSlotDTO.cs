using Microsoft.EntityFrameworkCore.Storage;

namespace Badminton.Web.DTO.TimeSlot
{
    public class TimeSlotDTO
    {
        public int TimeSlotId { get; set; }

        public TimeOnly StartTime { get; set; } 

        public TimeOnly EndTime { get; set; } 

        public string SlotType { get; set; }
    }
}
