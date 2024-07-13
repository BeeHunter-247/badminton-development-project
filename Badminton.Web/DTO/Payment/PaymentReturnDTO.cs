namespace Badminton.Web.DTO.Payment
{
    public class PaymentReturnDTO

    {
        public int? Id { get; set; }
        /// <summary>
        /// 00: Success
        /// 99: Unknown
        /// 10: Error
        /// </summary>
        public string? PaymentStatus { get; set; }
        public string? PaymentMessage { get; set; }
        /// <summary>
        /// Format: yyyyMMddHHmmss
        /// </summary>
        public string? PaymentDate { get; set; }
        public string? PaymentRefId { get; set; }
        public decimal? Amount { get; set; }
        public string? Signature { get; set; }
    }
}


