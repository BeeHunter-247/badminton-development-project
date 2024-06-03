using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.TimeSlot
{
    public class CreateTimeSlotDTO
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string SlotType { get; set; }

    }
}
