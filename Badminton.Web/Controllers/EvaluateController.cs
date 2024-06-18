using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluateController : ControllerBase
    {
        private readonly IEvaluateRepository _evaluateRepo;
        private readonly ICourtRepository _courtRepo;
        private readonly IMapper _mapper;

        public EvaluateController(IEvaluateRepository evaluateRepo, ICourtRepository courtRepo, IMapper mapper)
        {
            _evaluateRepo = evaluateRepo;
            _courtRepo = courtRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var evaluates = await _evaluateRepo.GetAllAsync();
            var evaluateDTO = _mapper.Map<List<EvaluateDTO>>(evaluates);
            return Ok(evaluateDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var evaluate = await _evaluateRepo.GetByIdAsync(id);
            if(evaluate == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EvaluateDTO>(evaluate));
        }

        [HttpPost("{courtId:int}")]
        public async Task<IActionResult> Create([FromRoute] int courtId, CreateEvaluateDTO evaluateDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _courtRepo.CourtExist(courtId))
            {
                return BadRequest("Sân không tồn tại!");
            }

            var evaluateModel = _mapper.Map<Evaluate>(evaluateDTO);
            evaluateModel.CourtId = courtId;
            await _evaluateRepo.CreateAsync(evaluateModel);
            return CreatedAtAction(nameof(GetById), new {id = evaluateModel.EvaluateId}, _mapper.Map<EvaluateDTO>(evaluateModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEvaluateDTO updateDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var evaluate = await _evaluateRepo.UpdateAsync(id, updateDTO);
            if(evaluate == null) 
            { 
                return NotFound("Không tìm thấy sân!");
            }

            return Ok(_mapper.Map<EvaluateDTO>(evaluate));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var evaluateModel = await _evaluateRepo.DeleteAsync(id);

            if(evaluateModel == null)
            {
                return NotFound("Đánh giá không tồn tại!");
            }

            return NoContent();
        }
    }
}
