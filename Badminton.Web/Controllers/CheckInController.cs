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
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly IMapper _mapper;

        public CheckInController(ICheckInRepository checkInRepository, IMapper mapper)
        {
            _checkInRepository = checkInRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
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

            var checkIns = await _checkInRepository.GetAllAsync();
            var checkInDTO = _mapper.Map<List<CheckInDTO>>(checkIns);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = checkInDTO
            });
        }

        [HttpGet("{id}")]
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

            var checkIn = await _checkInRepository.GetByIdAsync(id);

            if (checkIn == null)
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
                Data = _mapper.Map<CheckInDTO>(checkIn)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCheckInDTO checkInDTO)
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
                var checkInModel = new CheckIn
                {
                    SubCourtId = checkInDTO.SubCourtId,
                    BookingId= checkInDTO.BookingId,
                    CheckInTime= DateTime.Now,
                    CheckInStatus = false,
                    UserId = checkInDTO.UserId
                };

                await _checkInRepository.CreateAsync(checkInModel);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = _mapper.Map<CheckInDTO>(checkInModel)
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCheckInDTO checkInDTO)
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

            var checkIn = await _checkInRepository.UpdateAsync(id, checkInDTO);

            if (checkIn == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "CheckIn not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<CheckInDTO>(checkIn)
            });
        }


        [HttpDelete("{id}")]
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

            var checkIn = await _checkInRepository.DeleteAsync(id);

            if (checkIn == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "CheckIn does not exist!"
                });
            }

            return NoContent();
        }





    }
}
