using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IBookingRepository
    {
        // Create
        Task<Booking> CreateAsync(Booking bookingModel);
        // Read
        Task<List<Booking>> GetAll();
        Task<Booking?> GetById(int id);
        Task<List<Booking?>> GetByUserId(int id);
        Task<List<Booking?>> GetByStatus(BookingStatus status);
        Task<List<Booking>> GetBookingsByDateAndTimeSlot(DateOnly date, int timeSlotId);
        Task<Booking?> UpdateAsync1(Booking existingBooking); // Thêm phương thức mới

        Task CreateMultipleAsync(List<Booking> bookings);
        // Update
        Task<Booking?> UpdateAsync(int id, UpdateBookingDTO  bookingDTO);
        Task<Booking?> UpdateStatusAsync(int id, UpdateBookingStatusRequestDTO updateDto);
        Task UpdateBookingStatusAsync(int id, BookingStatus status);

        // cancel
        Task CancelBookingAsync(int bookingId);
        Task Cancel(int bookingId);

        //CheckIn
        Task CheckInBookingAsync(int bookingId);

        //Confirm
        Task ConfirmBookingAsync(int bookingId);

        // Delete
        Task<Booking?> DeleteAsync(int id);

        // Kiểm tra
        Task<bool> BookingExists(int id);
        Task<bool> BookingExistsAsync(int id);
        Task<bool> IsTimeSlotAvailableAsync(int subCourtId, int timeSlotId, DateOnly bookingDate);
        Task<Booking?> GetBookingByUserAndTime(int userId, int subCourtId, DateOnly bookingDate, int timeSlotId);
        Task<bool> IsIgnoringCancelledAsync(int subCourtId, int timeSlotId, DateOnly bookingDate);
        Task<decimal> GetCancelBookingPercentageAsync();
    }
}
