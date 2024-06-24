using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateBookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();

            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<ScheduleDTO, Schedule>();
            CreateMap<CreateScheduleDTO, Schedule>()
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate));

            CreateMap<Promotion, PromotionDTO>();

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
