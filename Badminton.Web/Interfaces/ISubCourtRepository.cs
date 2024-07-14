using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface ISubCourtRepository
    {
        Task<List<SubCourt>> GetAllAsync(QueryOptions query);
        Task<SubCourt?> GetByIdAsync(int id);
        Task<SubCourt?> UpdateAsync(int id, UpdateSubCourtDTO sCourtDTO);
        Task<SubCourt> CreateAsync(SubCourt sCourtModel);
        Task<SubCourt?> DeleteAsync(int id);
        Task<IEnumerable<SubCourt>> CreateRangeAsync(IEnumerable<SubCourt> sCourtModels);
        Task<bool> CheckBookingExist(int subCourtId);
    }
}
