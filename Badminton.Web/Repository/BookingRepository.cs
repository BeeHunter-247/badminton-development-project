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

            
            existingBooking.BookingDate = DateOnly.Parse(bookingDTO.BookingDate);
            existingBooking.SubCourtId = bookingDTO.SubCourtId;
            existingBooking.TimeSlotId = bookingDTO.TimeSlotId;
            existingBooking.Amount= bookingDTO.Amount;
            await _context.SaveChangesAsync();

            return existingBooking;
        }

        public async Task<Booking?> UpdateStatusAsync(int id, UpdateBookingStatusDTO updateDto)
        {
            var existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking == null)
            {
                return null;
            }
            existingBooking.Status = (int)updateDto.Status;
            await _context.SaveChangesAsync();

            return existingBooking;
        }

        // cancel
        public async Task CancelBookingAsync (int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

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

        public async Task<bool> IsIgnoringCancelledAsync(int subCourtId, int timeSlotId, DateOnly bookingDate)
        {
            var bookings = await _context.Bookings
                .Where(b => b.SubCourtId == subCourtId && b.TimeSlotId == timeSlotId && b.BookingDate == bookingDate && b.Status != (int)BookingStatus.Cancelled)
                .ToListAsync();

            return !bookings.Any();
        }
    }
}
