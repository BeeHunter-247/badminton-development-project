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
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var sCourts = await _sCourtRepo.GetAllAsync(query);
            var sCourtDTO = _mapper.Map<List<SubCourtDTO>>(sCourts);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = sCourtDTO
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

            var sCourt = await _sCourtRepo.GetByIdAsync(id);
            if(sCourt == null)
            {
                return Ok(new ApiResponse
                {
                    Success= false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "SubCourt not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<SubCourtDTO>(sCourt)
            });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubCourtDTO updateDTO)
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

            var sCourt = await _sCourtRepo.UpdateAsync(id, updateDTO);
            if( sCourt == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "SubCourt not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<SubCourtDTO>(sCourt)
            });
        }

        [HttpPost("{courtId:int}")]
        public async Task<IActionResult> Create([FromRoute] int courtId, CreateSubCourtDTO createDTO)
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

            if(!await _courtRepo.CourtExist(courtId)) {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode= StatusCodes.Status400BadRequest,
                    Message = "Court does not exist!"
                });
            }

            var subCourts = new List<SubCourt>();

            for(int timeSlotId = 1; timeSlotId <= 7; timeSlotId++)
            {
                var subCourtModel = _mapper.Map<SubCourt>(createDTO);
                subCourtModel.CourtId = courtId;
                subCourtModel.TimeSlotId = timeSlotId;
                subCourts.Add(subCourtModel);
            }

            await _sCourtRepo.CreateRangeAsync(subCourts);
            return Ok(new ApiResponse
            {
                Success = true,
                StatusCode = StatusCodes.Status201Created,
                Message = "Created SubCourt successfully"
            }); 
        }

        [HttpDelete]
        [Route("CheckBookingExistAndDelete{subCourtId:int}")]
        public async Task<IActionResult> CheckBookingAndDeleteSubCourt([FromRoute] int subCourtId)
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

            var existBooking = await _sCourtRepo.CheckBookingExist(subCourtId);
            if(!existBooking)
            {
                var delete = await _sCourtRepo.DeleteAsync(subCourtId);

                if(delete != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Subcourt has been deleted successful."
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "SubCourt does not exist!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status409Conflict,
                Message = "The subcourt is booked, it cannot be deleted!"
            });
        }

        [HttpPut]
        [Route("{id}/UpdatePrice")]
        public async Task<IActionResult> UpdatePrice([FromRoute] int id, [FromBody] UpdatePriceDTO priceDTO)
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

            var update = await _sCourtRepo.UpdatePriceAsync(id, priceDTO);
            if(update == null)
            {
                return Ok(new ApiResponse
                {
                    Success= false,
                    Message = "SubCourt not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = update
            });
        }
    }
}
