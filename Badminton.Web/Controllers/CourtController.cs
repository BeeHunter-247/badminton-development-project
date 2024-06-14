using Badminton.Web.DTO.Court;
using Badminton.Web.Helpers;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtRepository _courtRepo;
        public CourtController(ICourtRepository courtRepo) 
        {
            _courtRepo = courtRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryCourt query)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courts = await _courtRepo.GetAllAsync(query);
            var courtDTO = courts.Select(c => c.ToFormatCourtDTO());
            return Ok(courtDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var court = await _courtRepo.GetByIdAsync(id);
            if(court == null) 
            { 
                return NotFound();
            }

            return Ok(court.ToFormatCourtDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courtModel = await _courtRepo.DeleteAsync(id);
            if(courtModel == null)
            {
                return NotFound("Sân không tồn tại!");
            }

            return Ok(courtModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourtDTO courtDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courtModel = courtDTO.ToFormatCourtFromCreate();
            await _courtRepo.CreateAsync(courtModel);
            return CreatedAtAction(nameof(GetById), new { id = courtModel.CourtId }, courtModel.ToFormatCourtDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCourtDTO courtDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courtModel = await _courtRepo.UpdateAsync(id, courtDTO);
            if(courtModel == null)
            {
                return NotFound();
            }

            return Ok(courtModel.ToFormatCourtDTO());
        }
    }
}
