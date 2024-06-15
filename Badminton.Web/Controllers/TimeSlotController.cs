﻿using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : Controller
    {
        private readonly ITimeSlotRepository _timeSlotRepo;
        private readonly IMapper _mapper;

        public TimeSlotController(ITimeSlotRepository timeSlotRepo, IMapper mapper)
        {
            _timeSlotRepo = timeSlotRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeSlot = await _timeSlotRepo.GetAllAsync();
            var timeSlotDTO = _mapper.Map<List<TimeSlotDTO>>(timeSlot);
            return Ok(timeSlotDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeSlot = await _timeSlotRepo.GetByIdAsync(id);
            if(timeSlot == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TimeSlotDTO>(timeSlot));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTimeSlotDTO timeSlotDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeSlot = await _timeSlotRepo.UpdateAsync(id, timeSlotDTO);
            if(timeSlot == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TimeSlotDTO>(timeSlot));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTimeSlotDTO timeSlotDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var timeSlotModel = new TimeSlot
                {
                    StartTime = TimeOnly.Parse(timeSlotDTO.StartTime),
                    EndTime = TimeOnly.Parse(timeSlotDTO.EndTime),
                    SlotType = timeSlotDTO.SlotType,
                };

                await _timeSlotRepo.CreateAsync(timeSlotModel);
                return Ok(_mapper.Map<TimeSlotDTO>(timeSlotModel));
            }
            catch
            {
                return BadRequest();
            }
            
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var timeSlotModel = await _timeSlotRepo.DeleteAsync(id);
            if(timeSlotModel == null)
            {
                return NotFound("TimeSlot không tồn tại!");
            }

            return NoContent();
        }
    }
}
