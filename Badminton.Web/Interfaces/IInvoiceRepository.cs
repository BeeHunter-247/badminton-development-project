using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int id);
        Task<Invoice> CreateAsync(Invoice invoice);
        Task<Invoice?> UpdateAsync(int id, UpdateInvoiceDTO invoiceDTO);
        Task<Invoice?> DeleteAsync(int id);
    }
}
