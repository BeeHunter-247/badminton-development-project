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
        public async Task<List<BookingDTO>> GetAll()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return _mapper.Map<List<BookingDTO>>(bookings);
        }
        public async Task<BookingDTO> GetById(int id)
        {
            var booking = await _context.Bookings
               .FirstOrDefaultAsync(b => b.BookingId== id);

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
        public async Task<Booking?> UpdateAsync(int id, UpdateBookingDTO bookingDTO)
        {
            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
            if (existingBooking == null)
            {
                return null;
            }

            
            existingBooking.BookingDate = DateOnly.Parse(bookingDTO.BookingDate);
            existingBooking.Status = (int)bookingDTO.Status;
            existingBooking.CancellationReason = bookingDTO.CancellationReason;
            await _context.SaveChangesAsync();

            return existingBooking;
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
                return null; 
            }

            booking.Status = (int)BookingStatus.Cancelled;
            booking.CancellationReason = cancellationReason;
            await _context.SaveChangesAsync();
            await _context.Entry(booking).ReloadAsync(); 
            return _mapper.Map<BookingDTO>(booking); 
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
