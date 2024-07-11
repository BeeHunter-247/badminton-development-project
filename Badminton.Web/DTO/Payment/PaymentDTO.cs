using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Web.DTO.Payment
{
    public class PaymentDTO
    {

        public int UserId { get; set; } // ID of the user making the payment

        public int? PromotionId { get; set; } // Optional ID of the promotion applied

        public decimal TotalPrice { get; set; } // Total amount to be paid

        public decimal? RefundAmount { get; set; } // Optional amount to be refunded

        public string PaymentMethod { get; set; } = string.Empty; // Payment method used (default empty string)

        public int PaymentStatus { get; set; } // Status of the payment (e.g., success, pending, failed)

        public DateTime PaymentDate { get; set; } = DateTime.Now; // Date and time when the payment was made (default to current time)

        public decimal? RequiredAmount { get; set; } // Optional amount required for payment

        public string PaymentContent { get; set; } = string.Empty; // Additional content related to the payment

        public string PaymentCurrency { get; set; } = string.Empty; // Currency used for the payment (default empty string)
    }
}
