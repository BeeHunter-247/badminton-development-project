using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;
        private readonly CourtSyncContext _context;
        private readonly ICourtRepository _courtRepo;

        public PromotionController(IPromotionRepository promotionRepository, IMapper mapper, CourtSyncContext context, ICourtRepository courtRepo)
        {
            _promotionRepository = promotionRepository;
            _mapper = mapper;
            _context = context;
            _courtRepo = courtRepo;
        }
        [HttpPost("{courtId:int}")]
        public async Task<IActionResult> Create(int courtId, CreatePromotionDTO promotionDTO)
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

            if (!await _courtRepo.CourtExist(courtId))
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Court does not exist!"
                });
            }

            var promotionModel = _mapper.Map<Promotion>(promotionDTO);
            promotionModel.CourtId = courtId;
            await _promotionRepository.CreateAsync(promotionModel);
            return CreatedAtAction(nameof(GetById), new { id = promotionModel.PromotionId}, new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<PromotionDTO>(promotionModel)
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionDTO>>> GetAll()
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
            var promotions = await _promotionRepository.GetAll();
            var promotionDTO = _mapper.Map<List<PromotionDTO>>(promotions);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = promotionDTO
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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

            var promotion = await _promotionRepository.GetByIdAsync(id);
            if (promotion == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Promotion mot found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = false,
                Data = _mapper.Map<PromotionDTO>(promotion)
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var promotion = await _promotionRepository.DeleteAsync(id);

            if (promotion == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Promotion does not exist !"
                });
            }
            return NoContent();


        }

    }
}
