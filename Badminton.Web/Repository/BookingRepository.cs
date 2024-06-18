using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;
using static Badminton.Web.DTO.BookingDTO;

namespace Badminton.Web.Repository
{
    public class BookingRepository : IBookingRepository
    {

        private readonly CourtSyncContext _context;
        private readonly IMapper _mapper;
        public BookingRepository (CourtSyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // create
        public async Task<Booking> CreateAsync(Booking bookingModel)
        {
            if (bookingModel == null)
            {
                throw new ArgumentNullException(nameof(bookingModel));
            }

            _context.Bookings.Add(bookingModel);
            await _context.SaveChangesAsync();
            return bookingModel;
        }

        //read
        /*public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .ToListAsync(); // Eager loading các đối tượng liên quan
        }
        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .FirstOrDefaultAsync(b => b.BookingId == id); // Eager loading các đối tượng liên quan
        }*/
        public async Task<List<BookingDTO>> GetAllAsync()
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .ToListAsync();

            return _mapper.Map<List<BookingDTO>>(bookings);
        }
        public async Task<BookingDTO> GetByIdAsync(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .FirstOrDefaultAsync(b => b.BookingId == id);


            return _mapper.Map<BookingDTO>(booking);
        }
        public async Task<List<BookingDTO>> GetBookingsByUserIdAsync(int userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .ToListAsync();
            return _mapper.Map<List<BookingDTO>>(bookings);
        }
        public async Task<List<BookingDTO>> GetBookingsBySubCourtIdAsync(int subCourtId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.SubCourtId == subCourtId)
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .ToListAsync();
            return _mapper.Map<List<BookingDTO>>(bookings);
        }

        //Update
        public async Task<Booking?> UpdateAsync(int id, Booking bookingToUpdate) // Nhận model Booking
        {
            // Tìm booking cần cập nhật dựa trên id
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return null; // Trả về null nếu không tìm thấy booking
            }

            // Cập nhật các thuộc tính của booking từ bookingToUpdate
            booking.BookingDate = bookingToUpdate.BookingDate;
            booking.Status = bookingToUpdate.Status;
            booking.CancellationReason = bookingToUpdate.CancellationReason;

            // Lưu các thay đổi vào cơ sở dữ liệu
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Xử lý xung đột cập nhật dữ liệu nếu cần
                throw;
            }

            return booking; // Trả về đối tượng Booking sau khi cập nhật
        }

        public async Task<bool> BookingExistsAsync(int id)
        {
            return await _context.Bookings.AnyAsync(e => e.BookingId == id);
        }


        public async Task<BookingDTO> CancelBookingAsync(int bookingId, string cancellationReason)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                return null; // Hoặc throw new NotFoundException("Booking not found");
            }

            booking.Status = (int)BookingStatus.Cancelled;
            booking.CancellationReason = cancellationReason;
            await _context.SaveChangesAsync();
            await _context.Entry(booking).ReloadAsync(); // Tải lại để có thông tin cập nhật

            return _mapper.Map<BookingDTO>(booking); // Trả về BookingDTO sau khi cập nhật
        }

        // Delete
        public async Task<Booking?> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return null;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        //Kiểm tra
        public async Task<bool> BookingExists(int id)
        {
            return await _context.Bookings.AnyAsync(b => b.BookingId == id);
        }
        public async Task<bool> IsTimeSlotAvailableAsync(int subCourtId, int timeSlotId, DateOnly bookingDate)
        {
            return !await _context.Bookings.AnyAsync(b =>
                b.SubCourtId == subCourtId &&
                b.TimeSlotId == timeSlotId &&
                b.BookingDate == bookingDate);
        }

        // tính toán
        public Task<decimal> CalculateBookingPriceAsync(int subCourtId, int timeSlotId, int promotionId)
        {
            throw new NotImplementedException();
        }


    }
}
