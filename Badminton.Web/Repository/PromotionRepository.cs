using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly CourtSyncContext _context;
        private readonly IMapper _mapper;
        public PromotionRepository(CourtSyncContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // create
        public async Task<Promotion> CreateAsync(Promotion promotion)
        {
            if(promotion == null) throw new ArgumentNullException(nameof(promotion));

            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }
        
        // read
        public async Task<List<PromotionDTO>> GetAll()
        {
            var promotions = await _context.Promotions.ToListAsync();
            return _mapper.Map<List<PromotionDTO>>(promotions);
        }
        public async Task<PromotionDTO?> GetByIdAsync(int id)
        {
            var promotion = await _context.Promotions
                .FirstOrDefaultAsync(p => p.PromotionId == id);

            return _mapper.Map<PromotionDTO>(promotion);
        }

        // update

        //delete
        public async Task<Promotion?> DeleteAsync(int id)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(e => e.PromotionId == id);
            if (promotion == null)
            {
                return null;
            }

            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }
    }
}
