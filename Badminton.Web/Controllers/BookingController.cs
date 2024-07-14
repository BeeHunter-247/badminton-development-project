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

        [HttpGet("userId/{id}", Name = "GetBookingByUserId")]
        public async Task<IActionResult> GetByUserIdAsync(int id)
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

            var booking = await _bookingRepo.GetByUserId(id);
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
                Data = _mapper.Map<List<BookingDTO>>(booking)


            });
         }

        [HttpGet("byDateAndTimeSlot")]
        public async Task<IActionResult> GetBookingsByDateAndTimeSlot( string date, [FromQuery] int timeSlotId)
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

            if (!DateOnly.TryParseExact(date, "yyyy-MM-dd", out var parsedDate))
            {
                return BadRequest("Invalid date format. Please use yyyy-MM-dd.");
            }

            var bookings = await _bookingRepo.GetBookingsByDateAndTimeSlot(parsedDate, timeSlotId);

            if (bookings.Count == 0)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No bookings found for the specified date and time slot."
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<List<BookingDTO>>(bookings) 
            });
        }


        [HttpGet("status/{status}", Name = "GetBookingByStatus")]
        public async Task<IActionResult> GetByStatusAsync(BookingStatus status)
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

            var booking = await _bookingRepo.GetByStatus(status);
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
                Data = _mapper.Map<List<BookingDTO>>(booking)
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

                // check booking trùng lặp
                var existingBooking = await _bookingRepo.GetBookingByUserAndTime(bookingDTO.UserId, bookingDTO.SubCourtId, parseBookingDate, bookingDTO.TimeSlotId);
                if (existingBooking != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = "The user already has a booking for this sub-court, date and time."
                    });
                }

                // check SubCourt khả dụng ko
                var isTimeSlotAvailable = await _bookingRepo.IsTimeSlotAvailableAsync(bookingDTO.SubCourtId, bookingDTO.TimeSlotId, parseBookingDate);
                if (!isTimeSlotAvailable)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = "SubCourt is unavailable on the specified date and time."
                    });
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
                    PromotionCode = (string.IsNullOrWhiteSpace(bookingDTO.PromotionCode) || bookingDTO.PromotionCode.Equals("string")) ? null : bookingDTO.PromotionCode,
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

            var existingBooking = await _bookingRepo.GetById(id);
            if (existingBooking == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Booking not found!"
                });
            }

            // check Pending
            if (existingBooking.Status != (int)BookingStatus.Pending)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "Only pending bookings can be updated."
                });
            }

            
            if (!DateOnly.TryParse(bookingDTO.BookingDate, out var parseBookingDate))
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "Invalid BookingDate format. Please use yyyy-MM-dd."
                });
            }

            var existingBookingCheck = await _bookingRepo.GetBookingByUserAndTime(
                existingBooking.UserId, bookingDTO.SubCourtId, parseBookingDate, bookingDTO.TimeSlotId);

            if (existingBookingCheck != null && existingBookingCheck.BookingId != id) // trừ cái booking đang cập nhật
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "The user already has a booking for this sub-court, date and time."
                });
            }

            // check SubCourt 
            var isTimeSlotAvailable = await _bookingRepo.IsTimeSlotAvailableAsync(
                bookingDTO.SubCourtId, bookingDTO.TimeSlotId, parseBookingDate);

            if (!isTimeSlotAvailable)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "SubCourt is unavailable on the specified date and time."
                });

            }

            var bookingU = await _bookingRepo.UpdateAsync(id, bookingDTO);

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<BookingDTO>(bookingU)
            });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatusBooking(int id, [FromForm] UpdateBookingStatusDTO updateDto)
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

            var existingBooking = await _bookingRepo.GetById(id);
            if (existingBooking == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Booking not found!"
                });
            }


            var bookingU = await _bookingRepo.UpdateStatusAsync(id, updateDto);

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<BookingDTO>(bookingU)
            });
        }


        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });

            try
            {
                var existingBooking = await _bookingRepo.GetById(id);
                if (existingBooking == null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Booking not found!"
                    });
                }

                // check Pending
                if (existingBooking.Status != (int)BookingStatus.Pending)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = "Only pending bookings can be cancel."
                    });
                }

                await _bookingRepo.CancelBookingAsync(id);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Booking canceled successfully."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
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
