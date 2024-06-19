using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        private readonly ILogger<BookingController> _logger;
        private readonly CourtSyncContext _context;

        // Constructor to inject IBookingRepository, IMapper, ILogger, and CourtSyncContext
        public BookingController(IBookingRepository bookingRepo, IMapper mapper, ILogger<BookingController> logger, CourtSyncContext context)
        {
            _bookingRepo = bookingRepo;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST: api/Booking

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBookingDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu dữ liệu đầu vào không hợp lệ
            }

            try
            {
                var booking = new Booking
                {
                    UserId = bookingDTO.UserId,
                    SubCourtId = bookingDTO.SubCourtId,
                    TimeSlotId = bookingDTO.TimeSlotId,
                    ScheduleId = bookingDTO.ScheduleId,
                    BookingDate = DateOnly.Parse(bookingDTO.BookingDate),
                    Status = (int)BookingStatus.Pending,
                    TotalPrice = (decimal)bookingDTO.TotalPrice,           // Bạn sẽ tính toán giá trị này sau
                    PromotionId = bookingDTO.PromotionId, // Nếu có khuyến mãi
                    InvoiceId = bookingDTO.InvoiceId,            // Giá trị ban đầu, sẽ cập nhật sau
                    PaymentId = bookingDTO.PaymentId            // Giá trị ban đầu, sẽ cập nhật sau
                };

                // Thêm logic tính toán TotalPrice và điền các trường khác ở đây ...

                await _bookingRepo.CreateAsync(booking);
                booking.TimeSlot = await _context.TimeSlots.FindAsync(booking.TimeSlotId);
                booking.SubCourt = await _context.SubCourts.FindAsync(booking.SubCourtId);


                return Ok(_mapper.Map<BookingDTO>(booking)); // Trả về BookingDTO
            }
            catch
            {
                return BadRequest(); // Trả về BadRequest nếu có lỗi
            }
        }




        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] int id, [FromBody] UpdateBookingDTO updateBookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updateBookingDto == null)
            {
                return BadRequest(); // Hoặc return NoContent();
            }

            try
            {
                // 1. Lấy booking hiện tại từ repository
                var bookingModle = await _bookingRepo.GetByIdAsync(id);
                if (bookingModle == null)
                {
                    return NotFound(); // Booking không tồn tại
                }

                /*// 2. Kiểm tra trạng thái của booking (chỉ cho phép cập nhật khi booking đang chờ)
                if (bookingModle.Status != BookingStatus.Pending) // Giả sử Pending là 0
                {
                    return BadRequest("Cannot update a booking that is not in pending status.");
                }

                // 3. Kiểm tra xem ngày cập nhật có hợp lệ không (không được ở quá khứ)
                if (DateOnly.TryParse(updateBookingDto.BookingDate, out var parsedDate) && parsedDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    return BadRequest("Booking date cannot be in the past.");
                }*/

                // 4. Ánh xạ UpdateBookingDTO vào existingBooking (chỉ ánh xạ các thuộc tính cần cập nhật)
                _mapper.Map(updateBookingDto, bookingModle);

                // 5. Cập nhật booking trong repository
                var updatedBooking = await _bookingRepo.UpdateAsync(id, updateBookingDto); // Truyền model Booking

                if (updatedBooking == null)
                {
                    return NotFound(); // Booking không tồn tại sau khi cập nhật (có thể do xung đột)
                }

                // 6. Trả về kết quả
                return Ok(_mapper.Map<BookingDTO>(updatedBooking)); // Trả về BookingDTO sau khi cập nhật
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bookingRepo.BookingExistsAsync(id))
                {
                    return NotFound(); // Booking không tồn tại
                }
                else
                {
                    throw; // Ném lại exception để xử lý ở tầng cao hơn
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating booking.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // DELETE: api/Booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingRepo.DeleteAsync(id);
            return NoContent();
        }


    }
}
