using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class InvoiceRepository
    {
        private readonly CourtSyncContext _context;
        private readonly IMapper _mapper;
        
        public InvoiceRepository(CourtSyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 

        //create
        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            if(invoice == null) throw new ArgumentNullException(nameof(invoice));
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        // read
        public async Task<List<InvoiceDTO>> GetAll()
        {
            var invoice = await _context.Invoices.ToListAsync();
            return _mapper.Map<List<InvoiceDTO>>(invoice);
        }

        public async Task<InvoiceDTO> GetByIdAsync(int id)
        {
            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(i => i.InvoiceId== id);

            return _mapper.Map<InvoiceDTO>(invoice);
        }

        //delete
        public async Task<Invoice> DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == id);
            if (invoice == null)
            {
                return null;
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        //update
        public async Task<Invoice> UpdateAsync(int id, UpdateInvoiceDTO invoiceDTO)
        {

            var existingInvoice= await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId== id);
            if (existingInvoice != null)
            {
                return null;
            }
            existingInvoice.Tax = invoiceDTO.Tax;
            existingInvoice.Discount = invoiceDTO.Discount;
            existingInvoice.TotalAmount = invoiceDTO.TotalAmount;

            await _context.SaveChangesAsync();
            return existingInvoice;
        }

    }
}
