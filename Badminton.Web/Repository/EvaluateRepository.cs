using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class EvaluateRepository : IEvaluateRepository
    {
        private readonly CourtSyncContext _context;
        public EvaluateRepository(CourtSyncContext context)
        {
            _context = context;
        }

        public async Task<Evaluate> CreateAsync(Evaluate evaluateModel)
        {
            await _context.AddAsync(evaluateModel);
            await _context.SaveChangesAsync();
            return evaluateModel;
        }

        public async Task<Evaluate?> DeleteAsync(int id)
        {
            var evaluateModel = await _context.Evaluates.FirstOrDefaultAsync(e => e.EvaluateId == id);

            if(evaluateModel == null)
            {
                return null;
            }

            _context.Evaluates.Remove(evaluateModel);
            await _context.SaveChangesAsync();
            return evaluateModel; 
        }

        public async Task<List<Evaluate>> GetAllAsync()
        {
            return await _context.Evaluates.ToListAsync();
        }

        public async Task<Evaluate> GetByIdAsync(int id)
        {
            return await _context.Evaluates.FirstOrDefaultAsync(e => e.EvaluateId == id);
        }

        public async Task<Evaluate?> UpdateAsync(int id, UpdateEvaluateDTO evaluateDTO)
        {
            var existingEvaluate = await _context.Evaluates.FirstOrDefaultAsync(e => e.EvaluateId == id);
            if(existingEvaluate == null) 
            {
                return null;
            }

            existingEvaluate.Rating = evaluateDTO.Rating;
            existingEvaluate.Comment = evaluateDTO.Comment;
            DateTime currentTime = DateTime.Now;
            existingEvaluate.EvaluateDate = currentTime;

            await _context.SaveChangesAsync();
            return existingEvaluate;
        }
    }
}
