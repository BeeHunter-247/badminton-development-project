using Badminton.Web.DTO.TimeSlot;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/timeSlot")]
    public class TimeSlotController : Controller
    {
        private readonly ITimeSlotRepository _timeSlotRepo;
        public TimeSlotController(ITimeSlotRepository timeSlotRepo)
        {
            _timeSlotRepo = timeSlotRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeSlot = await _timeSlotRepo.GetAllAsync();
            var timeSlotDTO = timeSlot.Select(t => t.ToFormatTimeSlotDTO());
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

            return Ok(timeSlot.ToFormatTimeSlotDTO());
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

            return Ok(timeSlot.ToFormatTimeSlotDTO());
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
                return Ok(timeSlotModel.ToFormatTimeSlotDTO());
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

            return Ok(timeSlotModel);
        }
    }
}
