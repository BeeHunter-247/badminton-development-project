﻿using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ICourtRepository _courtRepo;
        private readonly IUserRepository _userRepo;
        private readonly ISubCourtRepository _subCourtRepo;
        private readonly IMapper _mapper;
        private readonly CourtSyncContext _context;

        public BookingController(IBookingRepository bookingRepo, IMapper mapper, ISubCourtRepository subCourtRepository, ICourtRepository courtRepository, IUserRepository userRepository, CourtSyncContext context)
        {
            _bookingRepo = bookingRepo;
            _mapper = mapper;
            _courtRepo = courtRepository;
            _userRepo = userRepository;
            _subCourtRepo = subCourtRepository;
            _context = context;
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
        [HttpGet("summary")]
        public async Task<IActionResult> GetBookingSummary([FromQuery] int year, [FromQuery] int month, [FromQuery] int day)
        {
            if (year <= 0 || month <= 0 || month > 12 || day <= 0 || day > 31)
            {
                return BadRequest(new { message = "Invalid date parameters." });
            }

            // Tạo đối tượng DateTime cho ngày yêu cầu
            DateTime requestedDate = new DateTime(year, month, day);

            // Ngày đầu tiên của tháng và ngày yêu cầu
            DateTime firstDayOfMonth = new DateTime(requestedDate.Year, requestedDate.Month, 1);
            DateTime endDate = requestedDate; // Ngày tìm kiếm

            var validStatuses = new[] { 0, 1, 3 };

            // Lọc các bản ghi có ngày tạo nằm trong tháng và trước hoặc bằng ngày yêu cầu
            var summary = await _context.Bookings
                .Where(b => validStatuses.Contains(b.Status) &&
                            b.CreateDate >= firstDayOfMonth &&
                            b.CreateDate <= endDate) // Tính tổng doanh thu từ ngày đầu tháng đến ngày yêu cầu
                .GroupBy(b => 1)
                .Select(g => new BookingSummaryDto
                {
                    TotalAmount = g.Sum(b => b.Amount ?? 0),
                    TotalBookings = g.Count(),
                })
                .FirstOrDefaultAsync();

            if (summary == null)
            {
                return NotFound(new { message = "No bookings found for the specified date range." });
            }

            return Ok(summary);
        }












        [HttpGet("amount/{userId}")]
        public async Task<IActionResult> GetTotalAmountByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var validStatuses = new[] { 1, 3, 4 }; // Trạng thái hợp lệ

            var totalAmount = await _context.Bookings
                .Where(b => b.UserId == userId && validStatuses.Contains(b.Status))
                .SumAsync(b => b.Amount);

            if (totalAmount == null)
            {
                return NotFound("No bookings found for this user.");
            }

            return Ok(new { UserId = userId, TotalAmount = totalAmount });
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateAsync(CreateBookingDTO bookingDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "Invalid data",
        //            Data = ModelState
        //        });
        //    }

        //    try
        //    {
        //        if (!DateOnly.TryParse(bookingDTO.BookingDate, out var parseBookingDate))
        //        {
        //            ModelState.AddModelError("BookingDate", "Invalid BookingDate format. Please use yyyy-MM-dd.");
        //            return BadRequest(ModelState);
        //        }

        //        // check booking trùng lặp
        //        var existingBooking = await _bookingRepo.GetBookingByUserAndTime(bookingDTO.UserId, bookingDTO.SubCourtId, parseBookingDate, bookingDTO.TimeSlotId);
        //        if (existingBooking != null)
        //        {
        //            return Ok(new ApiResponse
        //            {
        //                Success = false,
        //                StatusCode = StatusCodes.Status409Conflict,
        //                Message = "The user already has a booking for this sub-court, date and time."
        //            });
        //        }

        //        // check SubCourt khả dụng ko
        //        var isTimeSlotAvailable = await _bookingRepo.IsTimeSlotAvailableAsync(bookingDTO.SubCourtId, bookingDTO.TimeSlotId, parseBookingDate);
        //        if (!isTimeSlotAvailable)
        //        {
        //            return Ok(new ApiResponse
        //            {
        //                Success = false,
        //                StatusCode = StatusCodes.Status409Conflict,
        //                Message = "SubCourt is unavailable on the specified date and time."
        //            });
        //        }

        //        var booking = new Booking

        //        {
        //            UserId = bookingDTO.UserId,
        //            SubCourtId = bookingDTO.SubCourtId,
        //            TimeSlotId = bookingDTO.TimeSlotId,
        //            CreateDate = DateTime.Parse(bookingDTO.CreateDate),
        //            BookingDate = parseBookingDate,
        //            Amount = bookingDTO.Amount,
        //            Status = (int)BookingStatus.Pending,
        //            BookingType = (int)BookingType.Daily,
        //            PromotionCode = (string.IsNullOrWhiteSpace(bookingDTO.PromotionCode) || bookingDTO.PromotionCode.Equals("string")) ? null : bookingDTO.PromotionCode,
        //        };


        //        await _bookingRepo.CreateAsync(booking);

        //        return Ok(new ApiResponse
        //        {
        //            Success = true,
        //            StatusCode = StatusCodes.Status201Created,
        //            Data = _mapper.Map<BookingDTO>(booking)
        //        });
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> CreateAsync1(CreateBookingDTO bookingDTO)
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

                // Check if the user already has a booking for the same sub-court, date, and time
                var existingBooking = await _bookingRepo.GetBookingByUserAndTime(bookingDTO.UserId, bookingDTO.SubCourtId, parseBookingDate, bookingDTO.TimeSlotId);
                if (existingBooking != null && existingBooking.Status != (int)BookingStatus.Cancelled)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = "The user already has a booking for this sub-court, date, and time."
                    });
                }


                // Check if the SubCourt is available
                var isTimeSlotAvailable = await _bookingRepo.IsTimeSlotAvailableAsync(bookingDTO.SubCourtId, bookingDTO.TimeSlotId, parseBookingDate);

                // check SubCourt khả dụng ko
                var isTimeSlotAvailable = await _bookingRepo.IsIgnoringCancelledAsync(bookingDTO.SubCourtId, bookingDTO.TimeSlotId, parseBookingDate);

                if (!isTimeSlotAvailable)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = "SubCourt is unavailable on the specified date and time."
                    });
                }

                // Get the SubCourt information
                var subCourt = await _subCourtRepo.GetByIdAsync(bookingDTO.SubCourtId);
                if (subCourt == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "SubCourt not found"
                    });
                }

                // Get the Court and Owner information
                var court = await _courtRepo.GetByIdAsync(subCourt.CourtId);
                if (court == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Court not found"
                    });
                }

                var owner = await _userRepo.GetUserByIdAsync(court.OwnerId);
                if (owner == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = $"Owner with ID {court.OwnerId} not found"
                    });
                }

                if (owner.RoleType != 1)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = $"Owner with ID {court.OwnerId} does not have the correct role type. Expected role type 2, but found {owner.RoleType}"
                    });
                }

                // Get the User information
                var user = await _userRepo.GetUserByIdAsync(bookingDTO.UserId);
                if (user == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = $"User with ID {bookingDTO.UserId} not found"
                    });
                }

                // Check if the User has enough balance
                if (user.AccountBalance < bookingDTO.Amount)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Insufficient account balance."
                    });
                }

                // Create the booking
                var booking = new Booking
                {
                    UserId = bookingDTO.UserId,
                    OwnerId = court.OwnerId,
                    SubCourtId = bookingDTO.SubCourtId,
                    TimeSlotId = bookingDTO.TimeSlotId,
                    CreateDate = DateTime.Parse(bookingDTO.CreateDate),
                    BookingDate = parseBookingDate,
                    Amount = bookingDTO.Amount,
                    Status = (int)BookingStatus.Pending,
                    BookingType = (int)BookingType.Daily,
                    PromotionCode = (string.IsNullOrWhiteSpace(bookingDTO.PromotionCode) || bookingDTO.PromotionCode.Equals("string")) ? null : bookingDTO.PromotionCode,
                };

                // Save the booking to the database
                await _bookingRepo.CreateAsync(booking);

                // Deduct the amount from the User's account balance
                user.AccountBalance -= bookingDTO.Amount;
                await _userRepo.UpdateAsync(user);

                // Add the amount to the Owner's account balance
                owner.AccountBalance += bookingDTO.Amount; // Assuming you want to add the entire amount
                await _userRepo.UpdateAsync(owner);

                return Ok(new ApiResponse
                {
                    Success = true,
                    StatusCode = StatusCodes.Status201Created,
                    Data = _mapper.Map<BookingDTO>(booking)
                });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Success = false,
                    Message = $"An error occurred while creating the booking: {ex.Message}"
                });
            }
        }






        //[HttpPost("create-multiple")]
        //public async Task<IActionResult> CreateMultipleBookingsAsync(CreateMultipleBookingDTO bookingDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "Invalid data",
        //            Data = ModelState
        //        });
        //    }

        //    if (bookingDTO.SubCourtIds == null || bookingDTO.SubCourtIds.Count < 3)
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "You must book at least three sub-courts."
        //        });
        //    }

        //    try
        //    {
        //        if (!DateOnly.TryParse(bookingDTO.BookingDate, out var parseBookingDate))
        //        {
        //            ModelState.AddModelError("BookingDate", "Invalid BookingDate format. Please use yyyy-MM-dd.");
        //            return BadRequest(ModelState);
        //        }

        //        var bookings = new List<Booking>();
        //        foreach (var subCourtId in bookingDTO.SubCourtIds)
        //        {
        //            foreach (var timeSlotId in bookingDTO.TimeSlotIds)
        //            {
        //                // Check for duplicate booking
        //                var existingBooking = await _bookingRepo.GetBookingByUserAndTime(bookingDTO.UserId, subCourtId, parseBookingDate, timeSlotId);
        //                if (existingBooking != null)
        //                {
        //                    return Ok(new ApiResponse
        //                    {
        //                        Success = false,
        //                        StatusCode = StatusCodes.Status409Conflict,
        //                        Message = $"The user already has a booking for sub-court {subCourtId} on the specified date and time."
        //                    });
        //                }

        //                // Check if the sub-court is available
        //                var isTimeSlotAvailable = await _bookingRepo.IsTimeSlotAvailableAsync(subCourtId, timeSlotId, parseBookingDate);
        //                if (!isTimeSlotAvailable)
        //                {
        //                    return Ok(new ApiResponse
        //                    {
        //                        Success = false,
        //                        StatusCode = StatusCodes.Status409Conflict,
        //                        Message = $"SubCourt {subCourtId} is unavailable on the specified date and time."
        //                    });
        //                }

        //                var booking = new Booking
        //                {
        //                    UserId = bookingDTO.UserId,
        //                    SubCourtId = subCourtId,
        //                    TimeSlotId = timeSlotId,
        //                    CreateDate = DateTime.Parse(bookingDTO.CreateDate),
        //                    BookingDate = parseBookingDate,
        //                    Amount = bookingDTO.Amount,
        //                    Status = (int)BookingStatus.Pending,
        //                    BookingType = (int)BookingType.Daily,
        //                    PromotionCode = string.IsNullOrWhiteSpace(bookingDTO.PromotionCode) || bookingDTO.PromotionCode.Equals("string") ? null : bookingDTO.PromotionCode,
        //                };

        //                bookings.Add(booking);
        //            }
        //        }

        //        await _bookingRepo.CreateMultipleAsync(bookings);

        //        var bookingDTOs = bookings.Select(b => _mapper.Map<BookingDTO>(b)).ToList();

        //        return Ok(new ApiResponse
        //        {
        //            Success = true,
        //            StatusCode = StatusCodes.Status201Created,
        //            Data = bookingDTOs
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "An error occurred while creating the bookings.",
        //            Data = ex.Message
        //        });
        //    }
        //}

=======
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

        //[HttpPut("{id}/cancel")]
        //public async Task<IActionResult> CancelBooking(int id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "Invalid data",
        //            Data = ModelState
        //        });

        //    try
        //    {
        //        var existingBooking = await _bookingRepo.GetById(id);
        //        if (existingBooking == null)
        //        {
        //            return Ok(new ApiResponse
        //            {
        //                Success = false,
        //                StatusCode = StatusCodes.Status404NotFound,
        //                Message = "Booking not found!"
        //            });
        //        }

        //        // check Pending
        //        if (existingBooking.Status != (int)BookingStatus.Pending)
        //        {
        //            return Ok(new ApiResponse
        //            {
        //                Success = false,
        //                StatusCode = StatusCodes.Status409Conflict,
        //                Message = "Only pending bookings can be cancel."
        //            });
        //        }

        //        await _bookingRepo.CancelBookingAsync(id);
        //        return Ok(new ApiResponse
        //        {
        //            Success = true,
        //            Message = "Booking canceled successfully."
        //        });
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new ApiResponse { Success = false, Message = ex.Message });
        //    }
        //}
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

                // Check if the booking status is pending
                if (existingBooking.Status != (int)BookingStatus.Pending)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status409Conflict,
                        Message = "Only pending bookings can be canceled."
                    });
                }

                // Get the user and owner details
                var user = await _userRepo.GetUserByIdAsync(existingBooking.UserId);
                var subCourt = await _subCourtRepo.GetByIdAsync(existingBooking.SubCourtId);
                var court = await _courtRepo.GetByIdAsync(subCourt.CourtId);
                var owner = await _userRepo.GetUserByIdAsync(court.OwnerId);

                if (user == null || owner == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "User or owner not found"
                    });
                }

                // Update the account balances
                user.AccountBalance += existingBooking.Amount;
                owner.AccountBalance -= existingBooking.Amount;

                // Save the updated user and owner details
                await _userRepo.UpdateAsync(user);
                await _userRepo.UpdateAsync(owner);

                // Cancel the booking
                existingBooking.Status = (int)BookingStatus.Cancelled;
                await _bookingRepo.UpdateAsync1(existingBooking);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Booking canceled successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Success = false,
                    Message = $"An error occurred while canceling the booking: {ex.Message}"
                });
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
