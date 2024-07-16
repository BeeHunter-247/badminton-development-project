using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IEvaluateRepository
    {
        Task<List<Evaluate>> GetAllAsync(QueryEvaluate query);
        Task<Evaluate?> GetByIdAsync(int id);
        Task<Evaluate> CreateAsync(Evaluate evaluateModel);
        Task<Evaluate?> UpdateAsync(int id, UpdateEvaluateDTO evaluateDTO);
        Task<Evaluate?> DeleteAsync(int id);
    }
}
