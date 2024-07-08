using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepo;
        private readonly IMapper _mapper;

        public PromotionController(IPromotionRepository promotionRepo, IMapper mapper)
        {
            _promotionRepo = promotionRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var promotions = await _promotionRepo.GetAllAsync();
            var promotionDTO = _mapper.Map<List<PromotionDTO>>(promotions);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = promotionDTO
            });
        }

        [HttpGet]
        [Route("{id:int}")]
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

            var promotion = await _promotionRepo.GetByIdAsync(id);

            if(promotion == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Promotion not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<PromotionDTO>(promotion)
            });
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
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

            var promotion = await _promotionRepo.GetByCodeAsync(code);

            if (promotion == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Promotion not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<PromotionDTO>(promotion)
            });
        }

        [HttpDelete]
        [Route("{id:int}")]
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

            var promotion = await _promotionRepo.DeleteAsync(id);

            if( promotion == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Promotion does not exist!"
                });
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionDTO promotionDTO)
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
                var promotionModel = new Promotion
                {
                    PromotionCode = promotionDTO.PromotionCode,
                    Description = promotionDTO.Description,
                    Percentage = promotionDTO.Percentage,
                    StartDate = DateOnly.Parse(promotionDTO.StartDate),
                    EndDate = DateOnly.Parse(promotionDTO.EndDate)
                };

                await _promotionRepo.CreateAsync(promotionModel);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = _mapper.Map<PromotionDTO>(promotionModel)
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePromotionDTO promotionDTO)
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

            var promotion = await _promotionRepo.UpdateAsync(id, promotionDTO);

            if(promotion == null)
            {
                return NotFound(new ApiResponse
                {
                    Success= false,
                    Message = "Promotion not found!"
                });  
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<PromotionDTO>(promotion)
            });
        }
    }
}
