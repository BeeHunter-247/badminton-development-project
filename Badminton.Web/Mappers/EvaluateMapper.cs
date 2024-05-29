using Badminton.Web.DTO.Evaluate;
using Badminton.Web.Models;

namespace Badminton.Web.Mappers
{
    public static class EvaluateMapper
    {
        public static EvaluateDTO ToFormatEvaluateDTO(this Evaluate evaluateModel)
        {
            return new EvaluateDTO
            {
                EvaluateId = evaluateModel.EvaluateId,
                CourtId = evaluateModel.CourtId,
                Rating = evaluateModel.Rating,
                Comment = evaluateModel.Comment,
                EvaluateDate = evaluateModel.EvaluateDate,
                UserId = evaluateModel.UserId,
            };
        }

        public static Evaluate ToFormatEvaluateFromCreate(this CreateEvaluateDTO evaluateDTO, int courtId)
        {
            return new Evaluate
            {
                CourtId = courtId,
                Rating = evaluateDTO.Rating,
                Comment = evaluateDTO.Comment,
                EvaluateDate = evaluateDTO.EvaluateDate,
                UserId = evaluateDTO.UserId
            };
        }
    }
}
