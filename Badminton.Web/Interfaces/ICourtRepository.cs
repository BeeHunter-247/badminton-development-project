using Badminton.Web.DTO.Court;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface ICourtRepository
    {
        Task<List<Court>> GetAllAsync();
        Task<Court?> GetByIdAsync(int id);
        Task<Court> CreateAsync(Court courtModel);
        Task<Court?> DeleteAsync(int id);
        Task<Court?> UpdateAsync(int id, UpdateCourtDTO courtDTO);
    }
}
