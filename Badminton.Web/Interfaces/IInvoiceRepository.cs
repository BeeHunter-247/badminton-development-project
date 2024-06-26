using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<InvoiceDTO>> GetAll();
        Task<InvoiceDTO> GetById(int id);
        Task<Invoice> Create(Invoice invoice);
        Task<Invoice> Update(int id, UpdateInvoiceDTO invoiceDTO);

    }
}
