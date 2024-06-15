using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCourtController : ControllerBase
    {
        private readonly ISubCourtRepository _sCourtRepo;
        private readonly ICourtRepository _courtRepo;
        private readonly IMapper _mapper;

        public SubCourtController(ISubCourtRepository sCourtRepo, ICourtRepository courtRepo, IMapper mapper)
        {
            _sCourtRepo = sCourtRepo;
            _courtRepo = courtRepo; 
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryOptions query)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sCourts = await _sCourtRepo.GetAllAsync(query);
            var sCourtDTO = _mapper.Map<List<SubCourtDTO>>(sCourts);
            return Ok(sCourtDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sCourt = await _sCourtRepo.GetByIdAsync(id);
            if(sCourt == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SubCourtDTO>(sCourt));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubCourtDTO updateDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sCourt = await _sCourtRepo.UpdateAsync(id, updateDTO);
            if( sCourt == null)
            {
                return NotFound("Không tìm thấy sân!");
            }

            return Ok(_mapper.Map<SubCourtDTO>(sCourt));
        }

        [HttpPost("{courtId:int}")]
        public async Task<IActionResult> Create([FromRoute] int courtId, CreateSubCourtDTO createDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _courtRepo.CourtExist(courtId)) {
                return BadRequest("Sân không tồn tại!");
            }

            var sCourtModel = _mapper.Map<SubCourt>(createDTO);
            sCourtModel.CourtId = courtId;
            await _sCourtRepo.CreateAsync(sCourtModel);
            return CreatedAtAction(nameof(GetById), new { id = sCourtModel.CourtId }, _mapper.Map<SubCourtDTO>(sCourtModel));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sCourtModel = await _sCourtRepo.DeleteAsync(id);
            if(sCourtModel == null)
            {
                return NotFound("Sân không tồn tại!");
            }

            return NoContent();
        }
    }
}
