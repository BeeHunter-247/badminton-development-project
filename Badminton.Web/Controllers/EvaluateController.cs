﻿using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Interfaces;
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
        private readonly IUserRepository _userRepo;

        public EvaluateController(IEvaluateRepository evaluateRepo, ICourtRepository courtRepo,
            IMapper mapper, IUserRepository userRepo)
        {
            _evaluateRepo = evaluateRepo;
            _courtRepo = courtRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryEvaluate query)
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

            var evaluates = await _evaluateRepo.GetAllAsync(query);
            var evaluateDTO = _mapper.Map<List<EvaluateDTO>>(evaluates);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = evaluateDTO
            });
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
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

            var evaluate = await _evaluateRepo.GetByIdAsync(id);
            if(evaluate == null)
            {
                return Ok(new ApiResponse
                {
                    Success= false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Evaluate not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<EvaluateDTO>(evaluate)
            });
        }

        [HttpPost("{courtId:int}")]
        public async Task<IActionResult> Create([FromRoute] int courtId, CreateEvaluateDTO evaluateDTO)
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

            if(!await _courtRepo.CourtExist(courtId))
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Court does not exist!"
                });
            }

            var user = await _userRepo.GetUserByIdAsync(evaluateDTO.UserId);
            if(user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "User does not exist!"
                });
            }

            var evaluateModel = _mapper.Map<Evaluate>(evaluateDTO);
            evaluateModel.CourtId = courtId;
            DateTime currentTime = DateTime.Now;
            evaluateModel.EvaluateDate = currentTime;
            evaluateModel.CreatedBy = user.FullName;
            await _evaluateRepo.CreateAsync(evaluateModel);
            return CreatedAtAction(nameof(GetById), new {id = evaluateModel.EvaluateId}, new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<EvaluateDTO>(evaluateModel)
            });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEvaluateDTO updateDTO)
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

            var evaluate = await _evaluateRepo.UpdateAsync(id, updateDTO);
            if(evaluate == null) 
            { 
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Evaluate not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<EvaluateDTO>(evaluate)
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

            var evaluateModel = await _evaluateRepo.DeleteAsync(id);

            if(evaluateModel == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Evaluate does not exist!"
                });
            }

            return NoContent();
        }
    }
}
