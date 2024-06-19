using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Court, CourtDTO>();
            CreateMap<CreateCourtDTO, Court>();
            CreateMap<Evaluate, EvaluateDTO>();
            CreateMap<CreateEvaluateDTO, Evaluate>();
            CreateMap<UpdateEvaluateDTO, Evaluate>();
            CreateMap<SubCourt, SubCourtDTO>();
            CreateMap<CreateSubCourtDTO, SubCourt>();
            CreateMap<TimeSlot, TimeSlotDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
