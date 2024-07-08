using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly CourtSyncContext _context;

        public PromotionRepository(CourtSyncContext context)
        {
            _context = context;
        }
        public async Task<Promotion> CreateAsync(Promotion promotionModel)
        {
            await _context.AddAsync(promotionModel);
            await _context.SaveChangesAsync();
            return promotionModel;
        }

        public async Task<Promotion?> DeleteAsync(int id)
        {
            var promotionModel = await _context.Promotions.FirstOrDefaultAsync(p => p.PromotionId == id);
            if(promotionModel == null)
            {
                return null;
            }

            _context.Promotions.Remove(promotionModel);
            await _context.SaveChangesAsync();
            return promotionModel;
        }

        public async Task<List<Promotion>> GetAllAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion?> GetByIdAsync(int id)
        {
            return await _context.Promotions.FirstOrDefaultAsync(p => p.PromotionId == id);
        }

        public async Task<Promotion?> GetByCodeAsync(string code)
        {
            return await _context.Promotions.FirstOrDefaultAsync(pc => pc.PromotionCode == code);
        }

        public async Task<Promotion?> UpdateAsync(int id, UpdatePromotionDTO promotionDTO)
        {
            var existingPromotion = await _context.Promotions.FirstOrDefaultAsync(p => p.PromotionId == id);
            if (existingPromotion == null)
            {
                return null;
            }

            existingPromotion.PromotionCode = promotionDTO.PromotionCode;
            existingPromotion.Description = promotionDTO.Description;
            existingPromotion.Percentage = promotionDTO.Percentage;
            existingPromotion.StartDate = DateOnly.Parse(promotionDTO.StartDate);
            existingPromotion.EndDate = DateOnly.Parse(promotionDTO.EndDate);

            await _context.SaveChangesAsync();
            return existingPromotion;
        }
    }
}
