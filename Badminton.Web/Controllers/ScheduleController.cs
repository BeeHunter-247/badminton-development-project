using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using AutoMapper;


namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateBookingDTO scheduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSchedule = _mapper.Map<Schedule>(scheduleDto);
            return CreatedAtAction(nameof(GetScheduleById), new { id = createdSchedule.ScheduleId }, _mapper.Map<Schedule>(scheduleDto));
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] UpdateScheduleDTO updateScheduleDTO)
        {
            if (!ModelState.IsValid || id != updateScheduleDTO.ScheduleId)
            {
                return BadRequest(ModelState);
            }

            var scheduleToUpdate = await _scheduleRepository.GetById(id);
            if (scheduleToUpdate == null)
            {
                return NotFound();
            }

            // Update only properties that should be updated (excluding ID)
            scheduleToUpdate.BookingDate = updateScheduleDTO.BookingDate ?? scheduleToUpdate.BookingDate;
            scheduleToUpdate.StartTime = updateScheduleDTO.StartTime ?? scheduleToUpdate.StartTime;
            scheduleToUpdate.EndTime = updateScheduleDTO.EndTime ?? scheduleToUpdate.EndTime;
            scheduleToUpdate.TotalHours = updateScheduleDTO.TotalHours ?? scheduleToUpdate.TotalHours;
            scheduleToUpdate.BookingType = updateScheduleDTO.BookingType ?? scheduleToUpdate.BookingType;

            var updatedSchedule = await _scheduleRepository.Update(id, scheduleToUpdate);
            var updatedScheduleDto = _mapper.Map<ScheduleDTO>(updatedSchedule);

            return Ok(updatedScheduleDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var deleted = await _scheduleRepository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _scheduleRepository.GetAll();
            var scheduleDtos = _mapper.Map<List<ScheduleDTO>>(schedules);
            return Ok(scheduleDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var schedule = await _scheduleRepository.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleDto = _mapper.Map<ScheduleDTO>(schedule);
            return Ok(scheduleDto);
        }
    }

}
