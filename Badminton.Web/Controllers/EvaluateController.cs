using Badminton.Web.DTO.Evaluate;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/evaluate")]
    [ApiController]
    public class EvaluateController : ControllerBase
    {
        private readonly IEvaluateRepository _evaluateRepo;
        private readonly ICourtRepository _courtRepo;
        public EvaluateController(IEvaluateRepository evaluateRepo, ICourtRepository courtRepo)
        {
            _evaluateRepo = evaluateRepo;
            _courtRepo = courtRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var evaluates = await _evaluateRepo.GetAllAsync();
            var evaluateDTO = evaluates.Select(e => e.ToFormatEvaluateDTO());
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

            return Ok(evaluate.ToFormatEvaluateDTO());
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

            var evaluateModel = evaluateDTO.ToFormatEvaluateFromCreate(courtId);
            await _evaluateRepo.CreateAsync(evaluateModel);
            return CreatedAtAction(nameof(GetById), new {id = evaluateModel.EvaluateId}, evaluateModel.ToFormatEvaluateDTO());
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

            return Ok(evaluate.ToFormatEvaluateDTO());
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

            return Ok(evaluateModel);
        }
    }
}
