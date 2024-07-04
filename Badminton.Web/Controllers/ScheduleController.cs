using Microsoft.AspNetCore.Http;
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
        public ScheduleController(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDTO scheduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
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
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = _mapper.Map<ScheduleDTO>(schedule)
                });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var schedule = await _scheduleRepository.Delete(id);

            if (schedule == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Schedule does not exist!"
                });
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var schedules = await _scheduleRepository.GetAll();
            var scheduleDTO = _mapper.Map<List<ScheduleDTO>>(schedules);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = scheduleDTO
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var schedule = await _scheduleRepository.GetById(id);

            if (schedule == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Schedule not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<ScheduleDTO>(schedule)
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateScheduleDTO scheduleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var schedule = await _scheduleRepository.Update(id, scheduleDTO);

            if (schedule == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Schedule not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<ScheduleDTO>(schedule)
            });
        }



    }

}
