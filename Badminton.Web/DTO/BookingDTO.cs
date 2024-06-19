using System.ComponentModel.DataAnnotations;
using Badminton.Web.Enums;

namespace Badminton.Web.DTO
{
    /*  public class BookingDTO
      {
          public int BookingId { get; set; }

          public int UserId { get; set; }

          public int SubCourtId { get; set; }

          public int TimeSlotId { get; set; }

          public int ScheduleId { get; set; }

          [DataType(DataType.Date)]   // chỉ định kiểu dl là ngày
          public DateOnly BookingDate { get; set; }

          public BookingStatus Status { get; set; } // sử dụng enum

          public string? CancellationReason { get; set; }

          [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be non-negative")]   // giá không âm
          public decimal TotalPrice { get; set; }

          public int? PromotionId { get; set; }

          public int InvoiceId { get; set; }

          public int PaymentId { get; set; }

          public TimeSlotDTO TimeSlot { get; set; }
          public SubCourtDTO SubCourt { get; set; }

          public string UserName { get; set; } // Tên người dùng
          public string SubCourtName { get; set; } // Tên sân
          public string TimeSlotDetails { get; set; } // Chi tiết khung giờ
          public string PromotionName { get; set; } // Tên khuyến mãi



      }*/
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

        public TimeSlotDTO TimeSlot { get; set; }

        public SubCourtDTO SubCourt { get; set; }

        public string Username { get; set; } // Tên người dùng

        //public string PromotionName { get; set; } // Tên khuyến mãi -- chưa làm 
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
       /* [DateInFuture(ErrorMessage = "Booking date must be in the future")] // Validate ngày trong tương lai*/
        public string BookingDate { get; set; } // Format: "yyyy-MM-dd"

        [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be non-negative")]
        public decimal? TotalPrice { get; set; } // Cho phép null để tính sau

        public int? PromotionId { get; set; } // Cho phép null nếu không có khuyến mãi
        public int InvoiceId { get; set; }
        public int PaymentId { get; set; }
    }

    public class UpdateBookingDTO
    {
        public String BookingDate { get; set; } // Cho phép null nếu không cập nhật ngày
        public BookingStatus? Status { get; set; } // Cho phép null nếu không cập nhật trạng thái

        [StringLength(255, ErrorMessage = "CancellationReason cannot exceed 255 characters")]
        public string? CancellationReason { get; set; } // Cho phép null nếu không hủy
    }

    /*public class DateInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Cho phép null
            }

            DateTime bookingDate = DateTime.Parse(value.ToString());
            if (bookingDate <= DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }*/
}
