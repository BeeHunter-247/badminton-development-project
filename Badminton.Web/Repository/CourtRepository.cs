using Badminton.Web.DTO.Court;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class CourtRepository : ICourtRepository
    {
        private readonly CourtSyncContext _context;
        public CourtRepository(CourtSyncContext context)
        {
            _context = context;
        }

        public async Task<bool> CourtExist(int id)
        {
            return await _context.Courts.AnyAsync(c => c.CourtId == id);
        }

        public async Task<Court> CreateAsync(Court courtModel)
        {
            await _context.AddAsync(courtModel);
            await _context.SaveChangesAsync();
            return courtModel;
        }

        public async Task<Court?> DeleteAsync(int id)
        {
            var courtModel = await _context.Courts.FirstOrDefaultAsync(c => c.CourtId == id);
            if(courtModel == null)
            {
                return null;
            }

            _context.Courts.Remove(courtModel);
            await _context.SaveChangesAsync();
            return courtModel;
        }

        public async Task<List<Court>> GetAllAsync()
        {
            return await _context.Courts.Include(e => e.Evaluates).ToListAsync();
        }

        public async Task<Court?> GetByIdAsync(int id)
        {
            return await _context.Courts.Include(e => e.Evaluates).FirstOrDefaultAsync(c => c.CourtId == id);
        }

        public async Task<Court?> UpdateAsync(int id, UpdateCourtDTO courtDTO)
        {
            var existingCourt = await _context.Courts.FirstOrDefaultAsync(c => c.CourtId == id);

            if (existingCourt == null)
            {
                return null;
            }

            existingCourt.CourtName = courtDTO.CourtName;
            existingCourt.Location = courtDTO.Location;
            existingCourt.Phone = courtDTO.Phone;
            existingCourt.OpeningHours = courtDTO.OpeningHours;
            existingCourt.PricePerHour = courtDTO.PricePerHour;
            existingCourt.Image = courtDTO.Image;
            existingCourt.Announcement = courtDTO.Announcement;

            await _context.SaveChangesAsync();
            return existingCourt;
        }
    }
}
