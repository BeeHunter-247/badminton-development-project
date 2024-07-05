using Badminton.Web.DTO;
using Badminton.Web.Enums;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class CheckInRepository : ICheckInRepository
    {
        private readonly CourtSyncContext _context;

        public CheckInRepository(CourtSyncContext context)
        {
            _context = context;
        }

        public async Task<CheckIn> CreateAsync(CheckIn checkInModel)
        {
            await _context.AddAsync(checkInModel);
            await _context.SaveChangesAsync();
            return checkInModel;
        }

        public async Task<List<CheckIn>> GetAllAsync()
        {
            return await _context.CheckIns.ToListAsync();
        }

        public async Task<CheckIn?> GetByIdAsync(int id)
        {
            return await _context.CheckIns.FirstOrDefaultAsync(c => c.CheckInId == id);
        }

        public async Task<CheckIn?> UpdateAsync(int id, UpdateCheckInDTO checkInDTO)
        {
            var existingCheckIn = await _context.CheckIns.FirstOrDefaultAsync(c => c.CheckInId == id);
            if (existingCheckIn == null)
            {
                return null;
            }

            existingCheckIn.CheckInStatus = checkInDTO.CheckInStatus;
            

            await _context.SaveChangesAsync();
            return existingCheckIn;
        }

        public async Task<CheckIn?> DeleteAsync(int id)
        {
            var checkInModel = await _context.CheckIns.FirstOrDefaultAsync(c => c.CheckInId == id);
            if (checkInModel == null)
            {
                return null;
            }

            _context.CheckIns.Remove(checkInModel);
            await _context.SaveChangesAsync();
            return checkInModel;
        }

    }
}
