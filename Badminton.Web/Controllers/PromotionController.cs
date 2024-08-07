﻿using AutoMapper;
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
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Promotion not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<PromotionDTO>(promotion)
            });
        }

        [HttpGet("{code}/alpha")]
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

            if (!int.TryParse(code, out _)) // check chuoi
            {
                var promotion = await _promotionRepo.GetByCodeAsync(code);

                if (promotion == null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Promotion not found!"
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = _mapper.Map<PromotionDTO>(promotion)
                });
            }
            else
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid promotion code format"
                });
            }
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
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
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
                var endDate = DateOnly.Parse(promotionDTO.EndDate);
                var startDate = DateOnly.Parse(promotionDTO.StartDate);
                var now = DateOnly.FromDateTime(DateTime.UtcNow);

                // Check EndDate
                if (endDate <= now)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "EndDate must be greater than today's date."
                    });
                }

                // Check StartDate 
                if (startDate > endDate)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "StartDate cannot be greater than EndDate."
                    });
                }

                var promotionModel = new Promotion
                {
                    PromotionCode = promotionDTO.PromotionCode,
                    CourtId = promotionDTO.CourtId,
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
                return Ok(new ApiResponse
                {
                    Success= false,
                    StatusCode = StatusCodes.Status404NotFound,
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
