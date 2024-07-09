using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
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
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
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
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Court does not exist!"
                });
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCourtDTO courtDTO)
        {
            var images = new List<string>();

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            /*if(courtDTO.formfile != null) 
            {
                var fileResult = _fileRepo.SaveImage(court);
                if (fileResult.Item1 == 1)
                {
                    courtDTO.Image = fileResult.Item2;
                }
            }
            */

            foreach (var file in courtDTO.formFiles)
            {
                var fileResult = _fileRepo.SaveImage(file);
                if (fileResult.Item1 == 1)
                {
                    images.Add(courtDTO.Image = fileResult.Item2);
                }

                courtDTO.Image = string.Join(", ", images);
            }

            var courtModel = _mapper.Map<Court>(courtDTO);

           /* var courtModel = new Court
            {
                CourtName = courtDTO.CourtName,
                CourtManagerId = courtDTO.CourtManagerId,
                Location = courtDTO.Location,
                Phone = courtDTO.Phone,
                OpeningHours = courtDTO.OpeningHours,
                Image = string.Join(", ", images),
                Announcement = courtDTO.Announcement,
            };
           */

            var success = await _courtRepo.CreateAsync(courtModel);
            if (success != null)
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
                    Success = false,
                    StatusCode = 0,
                    Message = "Error on adding Court"
                });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateCourtDTO courtDTO)
        {
            var images = new List<string>();

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            foreach (var file in courtDTO.formFiles)
            {
                var fileResult = _fileRepo.SaveImage(file);
                if (fileResult.Item1 == 1)
                {
                    images.Add(courtDTO.Image = fileResult.Item2);
                }

                courtDTO.Image = string.Join(", ", images);
            }

            var courtModel = await _courtRepo.UpdateAsync(id, courtDTO);
            if (courtModel == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Court not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                StatusCode = 1,
                Data = _mapper.Map<CourtDTO>(courtModel)
            });
        }
    }
}
