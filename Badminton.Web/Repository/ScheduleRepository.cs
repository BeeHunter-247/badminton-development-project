using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Enums;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Badminton.Web.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CourtSyncContext _context;
        private readonly IMapper _mapper;

        public ScheduleRepository(CourtSyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //get
        public async Task<List<ScheduleDTO>> GetAll()
        {
            var schedules = await _context.Schedules.ToListAsync();
            return _mapper.Map<List<ScheduleDTO>>(schedules);
        }
        public async Task<ScheduleDTO> GetById(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            return _mapper.Map<ScheduleDTO>(schedule);
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

        //update
        /*public async Task<ScheduleDTO> Update(int id, ScheduleDTO scheduleDto)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return null;
            }

            schedule.BookingDate = scheduleDto.BookingDate;
            schedule.StartTime = scheduleDto.StartTime;
            schedule.EndTime = scheduleDto.EndTime;
            schedule.TotalHours = scheduleDto.TotalHours;
            schedule.BookingType = BoookingType.scheduleDto.BookingType;

            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScheduleDTO>(schedule);
        }*/

        // delete
        public async Task<bool> Delete(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return false;
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ScheduleExistsAsync(Schedule scheduleCheck)
        {
            return await _context.Schedules.AnyAsync(s =>
                s.SubCourtId == scheduleCheck.SubCourtId &&
                s.BookingDate == scheduleCheck.BookingDate &&
                s.TimeSlotID == scheduleCheck.TimeSlotID);
        }
    }
}
