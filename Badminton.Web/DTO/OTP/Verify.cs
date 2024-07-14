namespace Badminton.Web.DTO.OTP
{
    public class Verify
    {
        public int Id { get; set; } // Primary key
        public string Email { get; set; }
        public string Otp { get; set; }
        // Thêm các thuộc tính khác nếu cần thiết
    }

}
