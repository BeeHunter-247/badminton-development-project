using Badminton.Web.DTO.SubCourt;
using Badminton.Web.Models;

namespace Badminton.Web.Mappers
{
    public static class SCourtMapper
    {
        public static SCourtDTO ToFormatSCourtDTO(this SubCourt sCourtModel)
        {
            return new SCourtDTO
            {
                SubCourtId = sCourtModel.SubCourtId,
                CourtId = sCourtModel.CourtId,
                Name = sCourtModel.Name,
                PricePerHour = sCourtModel.PricePerHour,
            };
        }

        public static SubCourt ToFormatSCourtFromCreate(this CreateSCourtDTO createSCourtDTO, int courtId)
        {
            return new SubCourt
            {
                CourtId = courtId,
                Name = createSCourtDTO.Name,
                PricePerHour = createSCourtDTO.PricePerHour
            };
        }
    }
}
