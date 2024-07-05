using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CourtSyncContext _context;
        public ScheduleRepository(CourtSyncContext context)
        {
            _context = context;
        }

        //get
        public async Task<List<Schedule>> GetAll()
        {
            return await _context.Schedules.ToListAsync();
        }
        public async Task<Schedule?> GetById(int id)
        {
            return await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId== id);
        }


        //create
        public async Task<Schedule> Create(Schedule scheduleModel)
        {
            if (scheduleModel == null)
            {
                throw new ArgumentException(nameof(scheduleModel));
            }

            _context.Schedules.Add(scheduleModel);
            await _context.SaveChangesAsync();
            return scheduleModel;
        }

        //Update
        public async Task<Schedule?> Update(int id, UpdateScheduleDTO scheduleDTO)
        {
            var existingSchedule = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == id);
            if (existingSchedule == null)
            {
                return null;
            }

            existingSchedule.SubCourtId = scheduleDTO.SubCourtId;
            existingSchedule.TimeSlotId = scheduleDTO.TimeSlotId;
            existingSchedule.BookingDate = DateOnly.Parse(scheduleDTO.BookingDate);
            existingSchedule.TotalHours = (decimal)scheduleDTO.TotalHours;
            existingSchedule.BookingType = (int)scheduleDTO.BookingType;

            await _context.SaveChangesAsync();
            return existingSchedule;
        }

        // delete

        public async Task<Schedule?> Delete(int id)
        {
            var scheduleModel = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == id);
            if (scheduleModel == null)
            {
                return null;
            }

            _context.Schedules.Remove(scheduleModel);
            await _context.SaveChangesAsync();
            return scheduleModel;
        }

        public async Task<bool> ScheduleExistsAsync(Schedule scheduleCheck)
        {
            return await _context.Schedules.AnyAsync(s =>
                s.SubCourtId == scheduleCheck.SubCourtId &&
                s.BookingDate == scheduleCheck.BookingDate &&
                s.TimeSlotId == scheduleCheck.TimeSlotId);
        }


    }
}
