using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepo, IMapper mapper)
        {
            _bookingRepo = bookingRepo;
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
            var bookings = await _bookingRepo.GetAll();
            var bookingDto = _mapper.Map<List<BookingDTO>>(bookings);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = bookingDto
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
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

            var booking = await _bookingRepo.GetById(id);
            if (booking == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Booking not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<BookingDTO>(booking)
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBookingDTO bookingDTO)
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

                if (!DateOnly.TryParse(bookingDTO.BookingDate, out var parseBookingDate))
                {
                    ModelState.AddModelError("BookingDate", "Invalid BookingDate format. Please use yyyy-MM-dd.");
                    return BadRequest(ModelState);
                }
                var booking = new Booking

                {
                    UserId = bookingDTO.UserId,
                    SubCourtId = bookingDTO.SubCourtId,
                    TimeSlotId = bookingDTO.TimeSlotId,
                    CreateDate = DateTime.Parse(bookingDTO.CreateDate),
                    BookingDate = parseBookingDate, 
                    Amount = bookingDTO.Amount,
                    Status = (int)BookingStatus.Pending,
                    BookingType = (int)BookingType.Daily,
                };


                await _bookingRepo.CreateAsync(booking);
                
                return Ok(new ApiResponse
                {
                    Success = true,
                    StatusCode = StatusCodes.Status201Created,
                    Data = _mapper.Map<BookingDTO>(booking)
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDTO bookingDTO)
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

            var bookingU = await _bookingRepo.UpdateAsync(id, bookingDTO);

            if (bookingU == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Booking not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<BookingDTO>(bookingU)
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

            var booking = await _bookingRepo.DeleteAsync(id);

            if (booking == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Booking does not exist!"
                });
            }

            return NoContent();
        }


    }
}
