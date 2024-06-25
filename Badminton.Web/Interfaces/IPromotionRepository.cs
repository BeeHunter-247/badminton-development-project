using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IPromotionRepository
    {
        // Create
        Task<Promotion> CreateAsync(Promotion promotion);
        // Read
        Task<List<PromotionDTO>> GetAll();
        Task<PromotionDTO?> GetByIdAsync(int id);
        // Update
        Task<Promotion?> UpdateAsync(int id, UpdatePromotionDTO promotionDTO);
        // Delete
        Task<Promotion?> DeleteAsync(int id);

    }
}
