﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using AutoMapper;
using Badminton.Web.Enums;


namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        private readonly CourtSyncContext _context;
        private readonly ILogger<BookingController> _logger;
        public ScheduleController(IScheduleRepository scheduleRepository, IMapper mapper, CourtSyncContext context, ILogger<BookingController> logger)
        {
            _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDTO scheduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!DateOnly.TryParse(scheduleDto.BookingDate, out var parsedBookingDate))
                {
                    ModelState.AddModelError("BookingDate", "Invalid BookingDate format. Please use yyyy-MM-dd.");
                    return BadRequest(ModelState);
                }

                var schedule = new Schedule
                {
                    UserId = scheduleDto.UserId,
                    SubCourtId = scheduleDto.SubCourtId,
                    BookingDate = parsedBookingDate,
                    TimeSlotId = scheduleDto.TimeSlotId,
                    TotalHours = scheduleDto.TotalHours,
                    BookingType = (int)BookingType.Daily
                };

                await _scheduleRepository.Create(schedule);

                return Ok(_mapper.Map<ScheduleDTO>(schedule));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating schedule.");
                return StatusCode(500, "Internal server error.");
            }
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
