using Badminton.Web.DTO.TimeSlot;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly CourtSyncContext _context;
        public TimeSlotRepository(CourtSyncContext context)
        {
            _context = context;
        }

        public async Task<TimeSlot> CreateAsync(TimeSlot timeSlotDTO)
        {
          
            await _context.AddAsync(timeSlotDTO);
            await _context.SaveChangesAsync();
            return timeSlotDTO;
        }

        public async Task<TimeSlot?> DeleteAsync(int id)
        {
            var timeSlot = await _context.TimeSlots.FirstOrDefaultAsync(t => t.TimeSlotId == id);
            if(timeSlot == null) 
            {
                return null;
            }

            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync();
            return timeSlot;
        }

        public async Task<List<TimeSlot>> GetAllAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public async Task<TimeSlot?> GetByIdAsync(int id)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(t => t.TimeSlotId == id);
        }

        public async Task<TimeSlot?> UpdateAsync(int id, UpdateTimeSlotDTO timeSlotDTO)
        {
            var existingTimeSlot = await _context.TimeSlots.FirstOrDefaultAsync(t => t.TimeSlotId == id);
            if (existingTimeSlot == null) 
            {
                return null;
            }
            
            existingTimeSlot.SubCourtId = timeSlotDTO.SubCourtId;
            existingTimeSlot.StartTime = TimeOnly.Parse(timeSlotDTO.StartTime);
            existingTimeSlot.EndTime = TimeOnly.Parse(timeSlotDTO.EndTime);
            existingTimeSlot.SlotType = timeSlotDTO.SlotType;

            await _context.SaveChangesAsync();
            return existingTimeSlot;
        }
        public async Task<TimeSlot> GetTime(TimeSlot timeSlot)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(t => t.TimeSlotId == timeSlot.TimeSlotId);
        }
    }
}
