using Badminton.Web.DTO;
using Badminton.Web.Helpers;
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

        public async Task<List<Court>> GetAllAsync(QueryCourt query)
        {
            var queryObject = _context.Courts.Include(s => s.SubCourts).Include(e => e.Evaluates).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(query.search))
            {
                queryObject = queryObject.Where(c => c.CourtName.ToLower().Contains(query.search.ToLower()) 
                || c.Location.ToLower().Contains(query.search.ToLower()));
            }
            #endregion

            #region Pagination
            var skipNumber = (query.pageNumber - 1) * query.pageSize;
            #endregion

            return await queryObject.Skip(skipNumber).Take(query.pageSize).ToListAsync();
        }

        public async Task<Court?> GetByIdAsync(int id)
        {
            return await _context.Courts.Include(e => e.Evaluates).Include(s => s.SubCourts).FirstOrDefaultAsync(c => c.CourtId == id);
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
            existingCourt.Image = courtDTO.Image;
            existingCourt.Announcement = courtDTO.Announcement;

            await _context.SaveChangesAsync();
            return existingCourt;
        }
    }
}
