using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
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
        public async Task<ScheduleDTO> Create(ScheduleDTO scheduleDto)
        {
            var schedule = _mapper.Map<Schedule>(scheduleDto);
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScheduleDTO>(schedule);
        }

        //update
        public async Task<ScheduleDTO> Update(int id, ScheduleDTO scheduleDto)
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
            schedule.BookingType = scheduleDto.BookingType;

            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScheduleDTO>(schedule);
        }

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

        // Các phương thức khác như Get, Delete...
    }
}
