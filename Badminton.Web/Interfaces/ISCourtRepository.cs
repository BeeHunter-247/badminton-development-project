using Badminton.Web.DTO.SubCourt;
using Badminton.Web.Helpers;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface ISCourtRepository
    {
        Task<List<SubCourt>> GetAllAsync(QuerySCourt query);
        Task<SubCourt?> GetByIdAsync(int id);
        Task<SubCourt?> UpdateAsync(int id, UpdateSCourtDTO sCourtDTO);
        Task<SubCourt> CreateAsync(SubCourt sCourtModel);
        Task<SubCourt?> DeleteAsync(int id);
    }
}
