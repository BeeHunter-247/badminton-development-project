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

        public decimal TotalPrice { get; set; } // Total amount to be paid
        public decimal? RequiredAmount { get; set; } // Optional amount required for payment
        public string PaymentRefId { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty; // Payment method used (default empty string)

        public string? PaymentStatus { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public DateTime? ExpireDate { get; set; }// Date and time when the payment was made (default to current time)
        public string? PaymentLanguage { get; set; } = string.Empty;
        public string? MerchantId { get; set; } = string.Empty;

        public string PaymentContent { get; set; } = string.Empty; // Additional content related to the payment

        public string? PaymentDestinationId { get; set; } = string.Empty;
        public decimal? PaidAmount { get; set; }

        public string PaymentCurrency { get; set; } = string.Empty; // Currency used for the payment (default empty string)
    }
}
