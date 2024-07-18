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
        public async Task<IActionResult> GetBookingsByDateAndTimeSlot(string date, [FromQuery] int timeSlotId)
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

        [HttpPost("create-multiple")]
        public async Task<IActionResult> CreateMultipleBookingsAsync(CreateMultipleBookingDTO bookingDTO)
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

            if (bookingDTO.SubCourtIds == null || bookingDTO.SubCourtIds.Count < 3)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "You must book at least three sub-courts."
                });
            }

            try
            {
                if (!DateOnly.TryParse(bookingDTO.BookingDate, out var parseBookingDate))
                {
                    ModelState.AddModelError("BookingDate", "Invalid BookingDate format. Please use yyyy-MM-dd.");
                    return BadRequest(ModelState);
                }

                var bookings = new List<Booking>();
                foreach (var subCourtId in bookingDTO.SubCourtIds)
                {
                    foreach (var timeSlotId in bookingDTO.TimeSlotIds)
                    {
                        // Check for duplicate booking
                        var existingBooking = await _bookingRepo.GetBookingByUserAndTime(bookingDTO.UserId, subCourtId, parseBookingDate, timeSlotId);
                        if (existingBooking != null)
                        {
                            return Ok(new ApiResponse
                            {
                                Success = false,
                                StatusCode = StatusCodes.Status409Conflict,
                                Message = $"The user already has a booking for sub-court {subCourtId} on the specified date and time."
                            });
                        }

                        // Check if the sub-court is available
                        var isTimeSlotAvailable = await _bookingRepo.IsTimeSlotAvailableAsync(subCourtId, timeSlotId, parseBookingDate);
                        if (!isTimeSlotAvailable)
                        {
                            return Ok(new ApiResponse
                            {
                                Success = false,
                                StatusCode = StatusCodes.Status409Conflict,
                                Message = $"SubCourt {subCourtId} is unavailable on the specified date and time."
                            });
                        }

                        var booking = new Booking
                        {
                            UserId = bookingDTO.UserId,
                            SubCourtId = subCourtId,
                            TimeSlotId = timeSlotId,
                            CreateDate = DateTime.Parse(bookingDTO.CreateDate),
                            BookingDate = parseBookingDate,
                            Amount = bookingDTO.Amount,
                            Status = (int)BookingStatus.Pending,
                            BookingType = (int)BookingType.Daily,
                            PromotionCode = string.IsNullOrWhiteSpace(bookingDTO.PromotionCode) || bookingDTO.PromotionCode.Equals("string") ? null : bookingDTO.PromotionCode,
                        };

                        bookings.Add(booking);
                    }
                }

                await _bookingRepo.CreateMultipleAsync(bookings);

                var bookingDTOs = bookings.Select(b => _mapper.Map<BookingDTO>(b)).ToList();

                return Ok(new ApiResponse
                {
                    Success = true,
                    StatusCode = StatusCodes.Status201Created,
                    Data = bookingDTOs
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while creating the bookings.",
                    Data = ex.Message
                });
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
        public async Task<IActionResult> UpdateStatusBooking(int id, [FromBody] UpdateBookingStatusRequestDTO requestDto)
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
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Booking not found!"
                });
            }

            try
            {
                // Tạo DTO với Id từ URL và trạng thái từ requestDto
                var updateDto = new UpdateBookingStatusRequestDTO
                {
                    Status = requestDto.Status
                };

                // Cập nhật trạng thái của booking
                var updatedBooking = await _bookingRepo.UpdateStatusAsync(id, updateDto);

                if (updatedBooking != null)
                {
                    var bookingDto = _mapper.Map<BookingDTO>(updatedBooking);
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Data = bookingDto
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Failed to update booking status."
                    });
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý lỗi phù hợp
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while updating booking status.",
                    Data = ex.Message
                });
            }
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

        [HttpPut("{id}/checkIn")]
        public async Task<IActionResult> CheckInBooking(int id)
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

                // check Confirm
                if (existingBooking.Status != (int)BookingStatus.Confirmed)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = "Only Confirmed bookings can be CheckIn."
                    });
                }

                await _bookingRepo.CheckInBookingAsync(id);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Booking CheckIn successfully."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}/confirm")]
        public async Task<IActionResult> ConfirmBooking(int id)
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
                        Message = "Only Pending bookings can be Confirm."
                    });
                }

                await _bookingRepo.ConfirmBookingAsync(id);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Booking Confirm successfully."
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
