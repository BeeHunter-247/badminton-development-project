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
            _bookingRepo = bookingRepo; 
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

        
        // PUT: api/Booking/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDTO.UpdateBookingDTO updateBookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // 1. Fetch the existing booking
                var existingBookingDto = await _bookingRepo.GetBookingByIdAsync(id);
                if (existingBookingDto == null)
                {
                    return NotFound();
                }

                // 2. Apply updates
                existingBookingDto.BookingDate = updateBookingDto.BookingDate ?? existingBookingDto.BookingDate;
                existingBookingDto.Status = updateBookingDto.Status ?? existingBookingDto.Status;
                existingBookingDto.CancellationReason = updateBookingDto.CancellationReason ?? existingBookingDto.CancellationReason;

                // 3. Update in the repository
                await _bookingRepo.UpdateBookingAsync(id, existingBookingDto); // Pass the updated BookingDTO

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/Booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingRepo.DeleteBookingAsync(id);
            return NoContent();
        }


    }
}
