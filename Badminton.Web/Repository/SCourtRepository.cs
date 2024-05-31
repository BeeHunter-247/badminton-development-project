using Badminton.Web.DTO.SubCourt;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class SCourtRepository : ISCourtRepository
    {
        private readonly CourtSyncContext _context;
        public SCourtRepository(CourtSyncContext context)
        {
            _context = context;
        }

        public async Task<SubCourt> CreateAsync(SubCourt sCourtModel)
        {
            await _context.SubCourts.AddAsync(sCourtModel);
            await _context.SaveChangesAsync();
            return sCourtModel;
        }

        public async Task<List<SubCourt>> GetAllAsync()
        {
            return await _context.SubCourts.ToListAsync();
        }

        public async Task<SubCourt?> GetByIdAsync(int id)
        {
            return await _context.SubCourts.FirstOrDefaultAsync(s => s.SubCourtId == id);
        }

        public async Task<SubCourt?> UpdateAsync(int id, UpdateSCourtDTO sCourtDTO)
        {
            var existingSCourt = await _context.SubCourts.FirstOrDefaultAsync(s => s.SubCourtId == id);
            if(existingSCourt == null)
            {
                return null;
            }

            existingSCourt.Name = sCourtDTO.Name;
            existingSCourt.PricePerHour = sCourtDTO.PricePerHour;
            await _context.SaveChangesAsync();
            return existingSCourt;
        }
    }
}
