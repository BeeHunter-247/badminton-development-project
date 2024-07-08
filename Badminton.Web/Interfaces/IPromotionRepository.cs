using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetAllAsync();
        Task<Promotion?> GetByIdAsync(int id);
        Task<Promotion?> GetByCodeAsync(string Code);
        Task<Promotion> CreateAsync(Promotion promotionModel);
        Task<Promotion?> UpdateAsync(int id, UpdatePromotionDTO promotionDTO);
        Task<Promotion?> DeleteAsync(int id);
    }
}
