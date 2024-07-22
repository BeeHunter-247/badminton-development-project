using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class BookingRepository : IBookingRepository
    {

        private readonly CourtSyncContext _context;
        
        public BookingRepository (CourtSyncContext context)
        {
            _context = context;
        }
        public async Task UpdateBookingStatusAsync(int id, BookingStatus status)
        {
            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
            if (existingBooking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            existingBooking.Status = (int)status;
            await _context.SaveChangesAsync();
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
        //
        public async Task CreateMultipleAsync(List<Booking> bookings)
        {
            await _context.Bookings.AddRangeAsync(bookings);
            await _context.SaveChangesAsync();
        }



        //read
        public async Task<List<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }
        public async Task<Booking?> GetById(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<List<Booking?>> GetByUserId(int id)
        {
            return await _context.Bookings.Where(u => u.UserId == id).ToListAsync();
        }

        public async Task<List<Booking?>> GetByStatus(BookingStatus status)
        {
            return await _context.Bookings.Where(b => (BookingStatus)b.Status == status).ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByDateAndTimeSlot(DateOnly date, int timeSlotId)
        {
            return await _context.Bookings
                .Where(b => b.BookingDate == date && b.TimeSlotId == timeSlotId)
                .ToListAsync();
        }

        //Update
        public async Task<Booking?> UpdateAsync(int id, UpdateBookingDTO bookingDTO)
        {
            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
            if (existingBooking == null)
            {
                return null;
            }

            // Cập nhật các thuộc tính của existingBooking từ bookingDTO
            existingBooking.BookingDate = DateOnly.Parse(bookingDTO.BookingDate); // Chuyển đổi string thành DateOnly
            existingBooking.SubCourtId = bookingDTO.SubCourtId;
            existingBooking.TimeSlotId = bookingDTO.TimeSlotId;
            existingBooking.Amount = bookingDTO.Amount;
            // Giả sử PromotionCode là một số nguyên, bạn có thể thay đổi nếu cần thiết
            existingBooking.PromotionCode = bookingDTO.PromotionCode?.ToString();

            await _context.SaveChangesAsync();
            return existingBooking;
        }

        public async Task<Booking?> UpdateAsync1(Booking existingBooking)
        {
            var bookingToUpdate = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == existingBooking.BookingId);
            if (bookingToUpdate == null)
            {
                return null;
            }

            // Cập nhật các thuộc tính của bookingToUpdate từ existingBooking
            bookingToUpdate.BookingDate = existingBooking.BookingDate;
            bookingToUpdate.SubCourtId = existingBooking.SubCourtId;
            bookingToUpdate.TimeSlotId = existingBooking.TimeSlotId;
            bookingToUpdate.Amount = existingBooking.Amount;
            bookingToUpdate.PromotionCode = existingBooking.PromotionCode;

            await _context.SaveChangesAsync();
            return bookingToUpdate;
        }


        // cancel
        public async Task CancelBookingAsync(int bookingId)
        {
            // Lấy thông tin của booking
            var booking = await _context.Bookings
                .Include(b => b.SubCourt) // Giả sử rằng SubCourt liên kết với Court và Court có OwnerId
                .ThenInclude(sc => sc.Court) // Lấy thông tin Court liên kết với SubCourt
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            // Kiểm tra trạng thái của booking
            if (booking.Status != (int)BookingStatus.Pending)
            {
                throw new InvalidOperationException("Only pending bookings can be cancelled.");
            }

            // Cập nhật số dư tài khoản của người dùng
            var user = await _context.Users.FindAsync(booking.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Cập nhật số dư tài khoản của chủ sở hữu
            var owner = await _context.Users.FindAsync(booking.SubCourt.Court.OwnerId);
            if (owner == null)
            {
                throw new KeyNotFoundException("Owner not found.");
            }

            // Hoàn tiền cho người dùng
            user.AccountBalance += booking.Amount;

            // Trừ tiền của chủ sở hữu
            owner.AccountBalance -= booking.Amount;

            // Cập nhật thông tin người dùng và chủ sở hữu trong cơ sở dữ liệu
            _context.Users.Update(user);
            _context.Users.Update(owner);

            // Cập nhật trạng thái của booking
            booking.Status = (int)BookingStatus.Cancelled;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while cancelling the booking.", ex);
            }
        }


        //CheckIn
        public async Task CheckInBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            booking.Status = (int)BookingStatus.CheckIn;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while cancelling the booking.", ex);
            }
        }

        //Confirm
        public async Task ConfirmBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            booking.Status = (int)BookingStatus.Confirmed;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while cancelling the booking.", ex);
            }
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

        // check
        public async Task<bool> BookingExistsAsync(int id)
        {
            return await _context.Bookings.AnyAsync(e => e.BookingId == id);
        }
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
        public async Task<Booking?> GetBookingByUserAndTime(int userId, int subCourtId, DateOnly bookingDate, int timeSlotId)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b =>
                    b.UserId == userId &&
                    b.SubCourtId == subCourtId &&
                    b.BookingDate == bookingDate &&
                    b.TimeSlotId == timeSlotId);
        }

        public async Task<Booking?> UpdateStatusAsync(int id, UpdateBookingStatusRequestDTO updateDto)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return null; // or throw an exception if necessary

            // Chuyển đổi từ enum BookingStatus sang kiểu int
            booking.Status = (int)updateDto.Status;

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<bool> IsIgnoringCancelledAsync(int subCourtId, int timeSlotId, DateOnly bookingDate)
        {
            var bookings = await _context.Bookings
                .Where(b => b.SubCourtId == subCourtId && b.TimeSlotId == timeSlotId && b.BookingDate == bookingDate && b.Status != (int)BookingStatus.Cancelled)
                .ToListAsync();

            return !bookings.Any();
        }
    }
}
