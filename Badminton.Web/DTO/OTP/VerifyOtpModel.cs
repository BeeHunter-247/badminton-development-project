using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.OTP
{
    public class VerifyOtpModel
    {
        public int Id { get; set; } // Primary key
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
