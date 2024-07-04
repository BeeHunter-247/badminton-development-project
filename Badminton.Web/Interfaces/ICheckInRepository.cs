using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface ICheckInRepository
    {
        Task<List<CheckIn>> GetAllAsync();
        Task<CheckIn?> GetByIdAsync(int id);
        Task<CheckIn> CreateAsync(CheckIn checkInModel);
        Task<CheckIn?> UpdateAsync(int id, UpdateCheckInDTO checkInDTO);
        Task<CheckIn?> DeleteAsync(int id);
    }
}
