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
        Task<List<BookingDTO>> GetAllAsync();
        Task<BookingDTO?> GetByIdAsync(int id);
        Task<List<BookingDTO>> GetBookingsByUserIdAsync(int userId); // Lấy danh sách các booking của một người dùng cụ thể
        Task<List<BookingDTO>> GetBookingsBySubCourtIdAsync(int subCourtId); //Lấy danh sách các booking trên một sân con cụ thể

        // Update
        Task<Booking?> UpdateAsync(int id, Booking booking);
        Task<bool> BookingExistsAsync(int id);
     
        Task<BookingDTO> CancelBookingAsync(int bookingId, string cancellationReason); //Hủy một booking đã tồn tại và cung cấp lý do hủy

        // Delete
        Task<Booking?> DeleteAsync(int id);

        // Kiểm tra
        Task<bool> BookingExists(int id); //  Kiểm tra xem một booking có tồn tại trong hệ thống hay không
        Task<bool> IsTimeSlotAvailableAsync(int subCourtId, int timeSlotId, DateOnly bookingDate);   //Kiểm tra xem một khung giờ trên một sân con có còn trống để đặt hay không

        // Tính toán
        Task<decimal> CalculateBookingPriceAsync(int subCourtId, int timeSlotId, int promotionId);  //Tính toán giá của một booking dựa trên sân con, khung giờ và chương trình khuyến mãi (nếu có).
    }
}
