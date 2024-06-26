using System.ComponentModel.DataAnnotations;
using Badminton.Web.Enums;

namespace Badminton.Web.DTO
{

    public class BookingDTO
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int SubCourtId { get; set; }

        public int TimeSlotId { get; set; }

        public int ScheduleId { get; set; }

        [DataType(DataType.Date)]
        public DateOnly BookingDate { get; set; }

        public BookingStatus Status { get; set; }

        public string? CancellationReason { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be non-negative")]
        public decimal TotalPrice { get; set; }

        public int? PromotionId { get; set; }

        public int InvoiceId { get; set; }

        public int PaymentId { get; set; }
    }

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
        
        [DataType(DataType.Date)]
        public string BookingDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be non-negative")]
        public decimal? TotalPrice { get; set; } // Cho phép null để tính sau

        public int? PromotionId { get; set; }
        public int InvoiceId { get; set; }
        public int PaymentId { get; set; }
    }

    public class UpdateBookingDTO
    {
        [DataType(DataType.Date)]
        public string BookingDate { get; set; } 
        public BookingStatus Status { get; set; } 

        [StringLength(255, ErrorMessage = "CancellationReason cannot exceed 255 characters")]
        public string? CancellationReason { get; set; } 
    }


}
