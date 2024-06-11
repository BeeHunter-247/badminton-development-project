using Badminton.Web.DTO.SubCourt;
using Badminton.Web.DTO.TimeSlot;
using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.Booking
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int SubCourtId { get; set; }
        public int TimeSlotId { get; set; }
        public int ScheduleId { get; set; }
        public DateOnly BookingDate { get; set; }
        public decimal TotalHours { get; set; }
        public string Status { get; set; }
        public string? CancellationReason { get; set; } // Cho phép null nếu không có lý do hủy
        public decimal TotalPrice { get; set; }
        public int PromotionId { get; set; }

        // DTOs liên quan
        //public UserDTO User { get; set; }
        public SCourtDTO SubCourt { get; set; }
        public TimeSlotDTO TimeSlot { get; set; }
        //public ScheduleDTO Schedule { get; set; }
        //public PromotionDTO Promotion { get; set; }
        //public List<CheckInDTO> CheckIns { get; set; }
        //public List<InvoiceDTO> Invoices { get; set; }
        //public List<PaymentDTO> Payments { get; set; }
        public class CreateBookingDTO
        {
            [Required(ErrorMessage = "UserId is required")]
            public int UserId { get; set; }

            [Required(ErrorMessage = "SubCourtId is required")]
            public int SubCourtId { get; set; }

            [Required(ErrorMessage = "TimeSlotId is required")]
            public int TimeSlotId { get; set; }

            [Required(ErrorMessage = "ScheduleId is required")]
            public int ScheduleId { get; set; }

            [Required(ErrorMessage = "BookingDate is required")]
            public DateOnly BookingDate { get; set; }
        }

        public class UpdateBookingDTO
        {
            public DateOnly? BookingDate { get; set; } // Cho phép null nếu không cập nhật ngày
            public string? Status { get; set; } // Cho phép null nếu không cập nhật trạng thái
            public string? CancellationReason { get; set; } // Cho phép null nếu không hủy
        }

    }
}
