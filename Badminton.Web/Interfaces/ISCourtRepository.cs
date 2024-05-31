using Badminton.Web.DTO.SubCourt;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface ISCourtRepository
    {
        Task<List<SubCourt>> GetAllAsync();
        Task<SubCourt?> GetByIdAsync(int id);
        Task<SubCourt?> UpdateAsync(int id, UpdateSCourtDTO sCourtDTO);
        Task<SubCourt> CreateAsync(SubCourt sCourtModel);
    }
}
