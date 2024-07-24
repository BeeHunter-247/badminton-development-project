using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface ICourtRepository
    {
        Task<List<Court>> GetAllAsync(QueryCourt query);
        Task<Court?> GetByIdAsync(int id);
        Task<Court> CreateAsync(Court courtModel);
        Task<Court?> DeleteAsync(int id);
        Task<Court?> UpdateAsync(int id, UpdateCourtDTO courtDTO);
        Task<bool> CourtExist(int id);
        Task<bool> CourtNameExist(string name);
        Task<Court?> GetCourtByIdAsync(int id);
    }
}
