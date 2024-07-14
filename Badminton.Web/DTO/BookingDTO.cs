using System.ComponentModel.DataAnnotations;
using Badminton.Web.Enums;

namespace Badminton.Web.DTO
{

    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int SubCourtId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        [DataType(DataType.Date)]
        public DateOnly BookingDate { get; set; }
        public int TimeSlotId { get; set; }
        public int BookingType { get; set; }
        public decimal? Amount { get; set; }
        public BookingStatus Status { get; set; }
        public string? CancellationReason { get; set; }
        public string? PromotionCode { get; set; }
    }

    public class CreateBookingDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "SubCourtId is required")]
        public int SubCourtId { get; set; }
        
        //[Required(ErrorMessage = "CreateDate is required")]

        [DataType(DataType.DateTime)]
        public string? CreateDate { get; set; }

        [Required(ErrorMessage = "BookingDate is required")]

        [DataType(DataType.Date)]
        public string? BookingDate { get; set; }

        [Required(ErrorMessage = "TimeSlotId is required")]
        public int TimeSlotId { get; set; }
        public decimal? Amount { get; set; }
        public string? PromotionCode { get; set; } = string.Empty;
    }

    public class UpdateBookingDTO
    {
        [Required]
        [DataType(DataType.Date)]
        public string BookingDate { get; set; }

        [Required(ErrorMessage = "SubCourtId is required")]
        public int SubCourtId { get; set; }

        [Required(ErrorMessage = "TimeSlotId is required")]
        public int TimeSlotId { get; set; }
        public decimal Amount { get; set; }
        public int? PromotionCode { get; set; }

    }

   /* public class CancelBookingDTO
    {
        [StringLength(255, ErrorMessage = "CancellationReason cannot exceed 255 characters")]
        public string? CancellationReason { get; set; }
       
    }*/
    public class UpdateBookingStatusDTO
    {
        public BookingStatus Status { get; set; }
    }
}
