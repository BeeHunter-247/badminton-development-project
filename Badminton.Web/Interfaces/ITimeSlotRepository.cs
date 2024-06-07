using Badminton.Web.DTO.TimeSlot;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<List<TimeSlot>> GetAllAsync();
        Task<TimeSlot?> GetByIdAsync(int id);
        Task<TimeSlot?> UpdateAsync(int id, UpdateTimeSlotDTO timeSlotDTO);
        Task<TimeSlot> CreateAsync(TimeSlot timeSlotModel);
        Task<TimeSlot?> DeleteAsync(int id);
        Task<TimeSlot> GetTime(TimeSlot timeSlot);

    }
}
