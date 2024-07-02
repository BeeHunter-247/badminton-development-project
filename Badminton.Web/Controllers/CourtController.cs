using AutoMapper;
using Badminton.Web.DTO;
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
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepo;

        public CourtController(ICourtRepository courtRepo, IMapper mapper, IFileRepository fileRepo)
        {
            _courtRepo = courtRepo;
            _mapper = mapper;
            _fileRepo = fileRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryCourt query)
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

            var courts = await _courtRepo.GetAllAsync(query);
            var courtDTO = _mapper.Map<List<CourtDTO>>(courts);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = courtDTO
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

            var court = await _courtRepo.GetByIdAsync(id);
            if (court == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Court not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<CourtDTO>(court)
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

            var courtModel = await _courtRepo.DeleteAsync(id);
            if (courtModel == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Court does not exist!"
                });
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCourtDTO courtDTO)
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

            if(courtDTO.formFile != null)
            {
                var fileResult = _fileRepo.SaveImage(courtDTO.formFile);
                if(fileResult.Item1 == 1)
                {
                    courtDTO.Image = fileResult.Item2;
                }

                var courtModel = _mapper.Map<Court>(courtDTO);
                var success = await _courtRepo.CreateAsync(courtModel);
                if(success != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        StatusCode = 1,
                        Message = "Added successfully",
                        Data = _mapper.Map<CourtDTO>(courtModel)
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success= false,
                        StatusCode = 0,
                        Message = "Error on adding Court"
                    });   
                }
            }

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateCourtDTO courtDTO)
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

            if(courtDTO.formFile != null)
            {
                var fileResult = _fileRepo.SaveImage(courtDTO.formFile);
                if (fileResult.Item1 == 1)
                {
                    courtDTO.Image = fileResult.Item2;
                }
            }

            var courtModel = await _courtRepo.UpdateAsync(id, courtDTO);
            if (courtModel == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Court not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<CourtDTO>(courtModel)
            });
        }
    }
}
