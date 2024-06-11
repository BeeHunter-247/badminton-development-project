using Badminton.Web.DTO.Booking;
using Badminton.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingController(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo; // Sửa đổi ở đây
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingRepo.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BookingDTO createBookingDto)
        {
            if (createBookingDto == null)
            {
                return BadRequest("Booking data is null.");
            }

            try
            {
                var createdBooking = await _bookingRepo.CreateBookingAsync(createBookingDto);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = createdBooking.BookingId }, createdBooking);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
