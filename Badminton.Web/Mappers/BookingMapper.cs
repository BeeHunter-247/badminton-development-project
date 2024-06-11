using Badminton.Web.DTO.Booking; 
using Badminton.Web.Models;
using static Badminton.Web.DTO.Booking.BookingDTO;

namespace Badminton.Web.Mappers
{
    public static class BookingMapper
    {
        public static BookingDTO ToFormatBookingDTO(this Booking bookingModel)
        {
            return new BookingDTO
            {
                BookingId = bookingModel.BookingId,
                UserId = bookingModel.UserId,
                SubCourtId = bookingModel.SubCourtId,
                TimeSlotId = bookingModel.TimeSlotId,
                ScheduleId = bookingModel.ScheduleId,
                BookingDate = bookingModel.BookingDate,
                TotalHours = bookingModel.TotalHours,
                Status = bookingModel.Status,
                CancellationReason = bookingModel.CancellationReason,
                TotalPrice = bookingModel.TotalPrice,
                PromotionId = bookingModel.PromotionId,

                SubCourt = bookingModel.SubCourt.ToFormatSCourtDTO(),
                TimeSlot = bookingModel.TimeSlot.ToFormatTimeSlotDTO()
            };
        }

        public static Booking ToFormatBookingFromCreate(this CreateBookingDTO createBookingDTO)
        {
            return new Booking
            {
                UserId = createBookingDTO.UserId,
                SubCourtId = createBookingDTO.SubCourtId,
                TimeSlotId = createBookingDTO.TimeSlotId,
                ScheduleId = createBookingDTO.ScheduleId,
                BookingDate = createBookingDTO.BookingDate,
                // Các thuộc tính khác như TotalHours, Status, TotalPrice, ... sẽ được xử lý sau
            };
        }
        public static Booking ToFormatBookingFromDTO(this BookingDTO bookingDto)
        {
            return new Booking
            {
                BookingId = bookingDto.BookingId,          // Giả sử BookingId có trong BookingDTO
                UserId = bookingDto.UserId,
                SubCourtId = bookingDto.SubCourtId,
                TimeSlotId = bookingDto.TimeSlotId,
                ScheduleId = bookingDto.ScheduleId,
                BookingDate = bookingDto.BookingDate,
                TotalHours = bookingDto.TotalHours,
                Status = bookingDto.Status,
                CancellationReason = bookingDto.CancellationReason,
                TotalPrice = bookingDto.TotalPrice,
                PromotionId = bookingDto.PromotionId,

                // Ánh xạ các đối tượng liên quan nếu cần (ví dụ: SubCourt, TimeSlot)
                //SubCourt = bookingDto.SubCourt?.ToFormatSCourt(),  // Ví dụ
                //TimeSlot = bookingDto.TimeSlot?.ToFormatTimeSlot(), // Ví dụ
            };
        }



    }
}
