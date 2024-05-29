using Badminton.Web.DTO.Court;
using Badminton.Web.Models;

namespace Badminton.Web.Mappers
{
    public static class CourtMapper
    {
        public static CourtDTO ToFormatCourtDTO(this Court courtModel)
        {
            return new CourtDTO
            {
                CourtId = courtModel.CourtId,
                CourtName = courtModel.CourtName,
                CourtManagerId = courtModel.CourtManagerId,
                Location = courtModel.Location,
                Phone = courtModel.Phone,
                OpeningHours = courtModel.OpeningHours,
                PricePerHour = courtModel.PricePerHour,
                Image = courtModel.Image,
                Announcement = courtModel.Announcement
            };
        }

        public static Court ToFormatCourtFromCreate(this CreateCourtDTO courtDTO)
        {
            return new Court
            {
                CourtName = courtDTO.CourtName,
                CourtManagerId = courtDTO.CourtManagerId,
                Location = courtDTO.Location,
                Phone = courtDTO.Phone,
                OpeningHours = courtDTO.OpeningHours,
                PricePerHour = courtDTO.PricePerHour,
                Image = courtDTO.Image,
                Announcement = courtDTO.Announcement
            };
        }
    }
}
