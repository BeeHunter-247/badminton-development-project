namespace Badminton.Web.DTO
{
    public class CreateMultipleBookingDTO
    {
        public int UserId { get; set; }
        public List<int> SubCourtIds { get; set; }
        public List<int> TimeSlotIds { get; set; } // Updated property for multiple time slot IDs
        public string BookingDate { get; set; }
        public string CreateDate { get; set; }
        public decimal Amount { get; set; }
        public string PromotionCode { get; set; }
    }
}
