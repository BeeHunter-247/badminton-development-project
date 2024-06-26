using Badminton.Web.DTO;
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
        Task<List<BookingDTO>> GetAll();
        Task<BookingDTO?> GetById(int id);

        Task<List<BookingDTO>> GetBookingsByUserIdAsync(int userId);
        Task<List<BookingDTO>> GetBookingsBySubCourtIdAsync(int subCourtId);

        // Update
        Task<Booking?> UpdateAsync(int id, UpdateBookingDTO  bookingDTO);
        Task<bool> BookingExistsAsync(int id);
     
        Task<BookingDTO> CancelBookingAsync(int bookingId, string cancellationReason);

        // Delete
        Task<Booking?> DeleteAsync(int id);

        // Kiểm tra
        Task<bool> BookingExists(int id);
        Task<bool> IsTimeSlotAvailableAsync(int subCourtId, int timeSlotId, DateOnly bookingDate);

        // Tính toán
        Task<decimal> CalculateBookingPriceAsync(int subCourtId, int timeSlotId, int promotionId);
    }
}
