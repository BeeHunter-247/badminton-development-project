using Badminton.Web.Models;

namespace Badminton.Web.DTO
{
    public class InvoiceDTO
    {
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Tax { get; set; }

        public decimal? Discount { get; set; }

        public decimal FinalAmount { get; set; }

        public DateTime InvoiceDate { get; set; }

    }

    public class CreateInvoiceDTO
    {
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Tax { get; set; }

        public decimal? Discount { get; set; }

    }

    public class UpdateInvoiceDTO
    {
        public decimal Tax { get; set; }

        public decimal? Discount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
