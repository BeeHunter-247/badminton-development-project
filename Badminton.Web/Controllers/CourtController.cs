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
        private readonly ISubCourtRepository _subCourtRepo;

        public CourtController(ICourtRepository courtRepo, IMapper mapper,
            IFileRepository fileRepo, ISubCourtRepository subCourtRepo)
        {
            _courtRepo = courtRepo;
            _mapper = mapper;
            _fileRepo = fileRepo;
            _subCourtRepo = subCourtRepo;
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

            var subCourts = await _subCourtRepo.GetByCourtIdAsync(id);

            var anySubCourtBooked = subCourts.Any(sc => _subCourtRepo.AnySubCourtBooked(sc.SubCourtId));

            if(anySubCourtBooked)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode= StatusCodes.Status400BadRequest,
                    Message = "Cannot delete court because one or more subcourts are booked!"
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

            if (courtDTO.FormFiles != null && courtDTO.FormFiles.Count > 0)
            {
                foreach (var file in courtDTO.FormFiles)
                {
                    var fileResult = _fileRepo.SaveImage(file);
                    if (fileResult.Item1 == 1)
                    {
                        images.Add(fileResult.Item2);
                    }
                }

                courtDTO.Image = string.Join(", ", images);
            }

            var courtTransfer = new SetDefaultStatus
            {
                CourtName = courtDTO.CourtName,
                OwnerId = courtDTO.OwnerId,
                Location = courtDTO.Location,
                Phone = courtDTO.Phone,
                OpeningHours = courtDTO.OpeningHours,
                Image = courtDTO.Image,
                Announcement = courtDTO.Announcement,
                FormFiles = courtDTO.FormFiles,
                Status = 0
            };

            var courtModel = _mapper.Map<Court>(courtTransfer);

            if (await _courtRepo.CourtNameExist(courtDTO.CourtName))
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "CourtName already exist!"
                });
            }

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

            var existingCourt = await _courtRepo.GetCourtByIdAsync(id);

            if(courtDTO.formFiles != null && courtDTO.formFiles.Any())
            {
                foreach (var file in courtDTO.formFiles)
                {
                    var fileResult = _fileRepo.SaveImage(file);
                    if (fileResult.Item1 == 1)
                    {
                        images.Add(courtDTO.Image = fileResult.Item2);
                    }

                    courtDTO.Image = string.Join(", ", images);
                }
            }
            else
            {
                courtDTO.Image = existingCourt.Image;
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

        [HttpPut]
        [Route("{id:int}/UpdateStatusCourt")]
        public async Task<IActionResult> UpdateStatusCourt([FromRoute] int id, [FromBody] UpdateStatusDTO statusDTO)
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

            var updateStatus = await _courtRepo.UpdateStatusAsync(id, statusDTO);
            if(updateStatus == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Court not found!",
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Data = updateStatus
            });
        }
    }
}
