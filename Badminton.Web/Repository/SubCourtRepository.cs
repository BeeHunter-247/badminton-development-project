using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class SubCourtRepository : ISubCourtRepository
    {
        private readonly CourtSyncContext _context;
        public SubCourtRepository(CourtSyncContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckBookingExist(int subCourtId)
        {
            return await _context.Bookings.AnyAsync(b => b.SubCourtId == subCourtId);
        }

        public async Task<SubCourt> CreateAsync(SubCourt sCourtModel)
        {
            await _context.SubCourts.AddAsync(sCourtModel);
            await _context.SaveChangesAsync();
            return sCourtModel;
        }

        public async Task<IEnumerable<SubCourt>> CreateRangeAsync(IEnumerable<SubCourt> sCourtModels)
        {
            await _context.SubCourts.AddRangeAsync(sCourtModels);
            await _context.SaveChangesAsync();
            return sCourtModels;
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

        public async Task<List<SubCourt>> GetAllAsync(QueryOptions query)
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

        public async Task<SubCourt?> UpdateAsync(int id, UpdateSubCourtDTO sCourtDTO)
        {
            var existingSCourt = await _context.SubCourts.FirstOrDefaultAsync(s => s.SubCourtId == id);
            if(existingSCourt == null)
            {
                return null;
            }

            existingSCourt.Name = sCourtDTO.Name;
            existingSCourt.PricePerHour = sCourtDTO.PricePerHour;
            existingSCourt.TimeSlotId = sCourtDTO.TimeSlotId;
            await _context.SaveChangesAsync();
            return existingSCourt;
        }
    }
}
