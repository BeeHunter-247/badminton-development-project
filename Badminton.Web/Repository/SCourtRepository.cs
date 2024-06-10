using Badminton.Web.DTO.SubCourt;
using Badminton.Web.Helpers;
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

        public async Task<SubCourt?> DeleteAsync(int id)
        {
            var sCourtModel = await _context.SubCourts.FirstOrDefaultAsync(s => s.SubCourtId == id);
            if(sCourtModel == null)
            {
                return null;
            }

            _context.SubCourts.Remove(sCourtModel);
            await _context.SaveChangesAsync();
            return sCourtModel;
        }

        public async Task<List<SubCourt>> GetAllAsync(QuerySCourt query)
        {
            var queryObject = _context.SubCourts.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(query.search))
            {
                queryObject = queryObject.Where(s => s.Name.ToLower().Contains(query.search.ToLower()));
            }

            if(query.from.HasValue)
            {
                queryObject = queryObject.Where(f => f.PricePerHour >= query.from.Value);
            }

            if(query.to.HasValue)
            {
                queryObject = queryObject.Where(t => t.PricePerHour <= query.to.Value);
            }
            #endregion

            #region Pagination
            var skipNumber = (query.pageNumber - 1) * query.pageSize;
            #endregion

            return await queryObject.Skip(skipNumber).Take(query.pageSize).ToListAsync();
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
