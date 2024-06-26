using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly CourtSyncContext _context;
        
        public InvoiceRepository(CourtSyncContext context)
        {
            _context = context;
        } 

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            await _context.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<Invoice?> DeleteAsync(int id)
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

        public async Task<Invoice?> UpdateAsync(int id, UpdateInvoiceDTO invoiceDTO)
        {

            var existingInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId== id);

            if (existingInvoice == null)
            {
                return null;
            }

            existingInvoice.Tax = invoiceDTO.Tax;
            existingInvoice.Discount = invoiceDTO.Discount;
            existingInvoice.TotalAmount = invoiceDTO.TotalAmount;
            existingInvoice.FinalAmount = invoiceDTO.FinalAmount;
            DateTime currentTime = DateTime.Now;
            existingInvoice.InvoiceDate = currentTime;
            await _context.SaveChangesAsync();
            return existingInvoice;
        }
    }
}
