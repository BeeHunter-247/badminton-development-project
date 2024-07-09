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
            return await _context.Bookings.FirstOrDefaultAsync(c => c.BookingId == id);
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

        // cancel
        public async Task CancelBookingAsync (int bookingId, string cancellationReason)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            booking.Status = (int)BookingStatus.Cancelled;
            booking.CancellationReason = cancellationReason;

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
    }
}
