using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IEvaluateRepository
    {
        Task<List<Evaluate>> GetAllAsync();
        Task<Evaluate?> GetByIdAsync(int id);
        Task<Evaluate> CreateAsync(Evaluate evaluateModel);
        Task<Evaluate?> UpdateAsync(int id, UpdateEvaluateDTO evaluateDTO);
        Task<Evaluate?> DeleteAsync(int id);
    }
}
