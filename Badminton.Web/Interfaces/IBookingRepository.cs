using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // Update
        Task<Booking?> UpdateAsync(int id, UpdateBookingDTO  bookingDTO);
        Task<Booking?> UpdateStatusAsync(int id, UpdateBookingStatusDTO updateDto);

        // cancel
        Task CancelBookingAsync(int bookingId);

        // Delete
        Task<Booking?> DeleteAsync(int id);

        // Kiểm tra
        Task<bool> BookingExists(int id);
        Task<bool> BookingExistsAsync(int id);
        Task<bool> IsTimeSlotAvailableAsync(int subCourtId, int timeSlotId, DateOnly bookingDate);
        Task<Booking?> GetBookingByUserAndTime(int userId, int subCourtId, DateOnly bookingDate, int timeSlotId);
    }
}
